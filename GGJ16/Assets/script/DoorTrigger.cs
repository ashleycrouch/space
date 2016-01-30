using UnityEngine;

public class DoorTrigger : MonoBehaviour {

	new Collider2D collider;

	Room room1, room2;

	void Awake() {
		collider = GetComponent<Collider2D>();
		foreach (Collider2D other in Physics2D.OverlapAreaAll(collider.bounds.min, collider.bounds.max)) {
			Room room = other.GetComponent<Room>();
			if (room) {
				if (!room1) {
					room1 = room;
				}
				else if (!room2)
				{
					room2 = room;
					break;
				}
			}
		}
		if (room1 == null || room2 == null) {
			Debug.LogWarning("Door is missing rooms!", this);
			gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("enter");
		if (other.tag == "Player") {
			if (Room.current == room1) {
				Room.current = room2;
			}
			else if (Room.current == room2)	{
				Room.current = room2;
			}
			else {
				Debug.LogWarning("Door does not connect to current room!", this);
			}
		}
	}
}