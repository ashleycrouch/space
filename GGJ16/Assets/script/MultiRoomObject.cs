using UnityEngine;
using System.Collections.Generic;

public class MultiRoomObject : MonoBehaviour {

	protected List<Room> rooms;

	new protected Collider2D collider;

	protected virtual void Awake() {
		rooms = new List<Room>();
		collider = GetComponent<Collider2D>();
		foreach (Collider2D other in Physics2D.OverlapAreaAll(collider.bounds.min, collider.bounds.max)) {
			Room room = other.GetComponent<Room>();
			if (room) {
				rooms.Add(room);
				room.AddMultiRoomObject(this);
			}
		}
	}

}