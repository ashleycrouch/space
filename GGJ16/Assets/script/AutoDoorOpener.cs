using UnityEngine;

public class AutoDoorOpener : MonoBehaviour {

	public Door door;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			door.Open();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			door.Close();
		}
	}

}