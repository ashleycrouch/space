﻿using UnityEngine;

public class DoorTrigger : MonoBehaviour {

	new Collider2D collider;

	Room room1, room2;

	//the direction moving from room 1 to room 2 (invert for other way)
	Vector2 direction;

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
		else {
			float dx = room2.transform.position.x - room1.transform.position.x;
			direction = Vector2.right * Mathf.Sign(dx);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			if (Room.current == room1) {
				Room.current = room2;
				MovePlayer(other, direction);
			}
			else if (Room.current == room2)	{
				Room.current = room1;
				MovePlayer(other, - direction);
			}
			else {
				Debug.LogWarning("Door does not connect to current room!", this);
			}
		}
	}

	void MovePlayer(Collider2D player, Vector2 direction) {
		float bufferDist = 0.05f; //extra distance to move player
		Vector3 playerPos = player.transform.position;
		if (direction == Vector2.left) {
			playerPos.x = transform.position.x - player.bounds.size.x - bufferDist;
			
		}
		if (direction == Vector2.right) {
			playerPos.x = transform.position.x + collider.bounds.size.x + bufferDist;
		}
		player.transform.position = playerPos;
	}
}