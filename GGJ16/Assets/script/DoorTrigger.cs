﻿using UnityEngine;

public class DoorTrigger : MultiRoomObject {

	public bool vertical = false;

	Room room1, room2;

	//the direction moving from room 1 to room 2 (invert for other way)
	Vector2 direction;
	Bounds bounds; //we need to cache the bounds so they can work when the gameobject isnt active

	protected override void Awake() {
		base.Awake();
		room1 = rooms.Count > 0 ? rooms[0] : null;
		room2 = rooms.Count > 1 ? rooms[1] : null;
		if (room1 == null || room2 == null) {
			Debug.LogWarning("Door is missing rooms!", this);
			collider.enabled = false;
		}
		else {
			if (!vertical) {
				float dx = room2.transform.position.x - room1.transform.position.x;
				direction = Vector2.right * Mathf.Sign(dx);
			}
			else {
				float dy = room2.transform.position.y - room1.transform.position.y;
				direction = Vector2.up * Mathf.Sign(dy);
			}
		}
		bounds = collider.bounds;
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.tag == "Player") {
			if (Room.current == room1) {
				Room.current = room2;
				MovePlayer(other, direction);
			}
			else if (Room.current == room2)	{
				Room.current = room1;
				MovePlayer(other, -direction);
			}
			else {
				Debug.LogWarning("Door does not connect to current room!", this);
			}
		}
	}

	void MovePlayer(Collider2D player, Vector2 direction) {
		float bufferDist = vertical ? 1f : .5f; //extra distance to move player
		Vector3 playerPos = player.transform.position;
		if (direction == Vector2.left) {
			playerPos.x = transform.position.x - player.bounds.size.x - bufferDist;
		}
		if (direction == Vector2.right) {
			playerPos.x = transform.position.x + bounds.size.x + bufferDist;
		}
		if (direction == Vector2.down) {
			playerPos.y = transform.position.y - player.bounds.size.y - bufferDist;
		}
		if (direction == Vector2.up) {
			playerPos.y = transform.position.y + bounds.size.y + bufferDist;
		}
		player.transform.position = playerPos;
	}
}