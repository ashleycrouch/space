﻿using UnityEngine;
using System.Collections.Generic;
using System;

public class Room : MonoBehaviour {

	static Room _current;
	public static Room current {
		get {
			return _current;
		}
		set {
			if (_current) {
				foreach (MultiRoomObject obj in _current.multiRoomObjects) {
					obj.gameObject.SetActive(false);
				}
				_current.gameObject.SetActive(false);
			}
			_current = value;
			if (_current) {
				foreach (MultiRoomObject obj in _current.multiRoomObjects) {
					obj.gameObject.SetActive(true);
				}
				_current.gameObject.SetActive(true);
			}
			if (onRoomChange != null) {
				onRoomChange(_current);
			}
		}
	}

	public static event Action<Room> onRoomChange;

	public const int WIDTH = 16;
	public const int HEIGHT = 12;

	public BoxCollider2D bounds { get; private set; }

	static List<Room> rooms;
	public static IEnumerable<Room> allRooms { get { return rooms; } }

	public string roomName = "<roomName>";

	List<MultiRoomObject> multiRoomObjects;

	void Awake() {
		if (rooms == null) {
			rooms = new List<Room>();
		}
		rooms.Add(this);
		multiRoomObjects = new List<MultiRoomObject>();
		bounds = gameObject.AddComponent<BoxCollider2D>();
		bounds.size = new Vector2(WIDTH, HEIGHT);
		bounds.offset = bounds.size / 2;
	}

	void Start() {
		int borderWidth = 1;
		GameObject parent = new GameObject("border walls");
		Transform parentTransform = parent.transform;
		parentTransform.parent = transform;
		parentTransform.localPosition = Vector3.zero;
		//left wall
		CreateBorderWall(parentTransform, -borderWidth, 0, borderWidth, HEIGHT);
		//right wall
		CreateBorderWall(parentTransform, WIDTH, 0, borderWidth, HEIGHT);
		//bottom wall
		CreateBorderWall(parentTransform, -borderWidth, -borderWidth, WIDTH + borderWidth * 2, borderWidth);
		//top wall
		CreateBorderWall(parentTransform, -borderWidth, HEIGHT, WIDTH + borderWidth * 2, borderWidth);
		current = this;
		SetCurrentFromPlayer();
	}

	void OnDrawGizmos() {
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.color = current == this ? Color.blue : Color.white;
		Vector3 size = new Vector3(WIDTH, HEIGHT, 0);
		Gizmos.DrawWireCube(size / 2, size);
	}

	GameObject CreateBorderWall(Transform parent, int x, int y, int w, int h) {
		GameObject obj = new GameObject("border wall");
		obj.transform.parent = parent;
		obj.transform.localPosition = new Vector3(x, y, 0);
		BoxCollider2D collider = obj.AddComponent<BoxCollider2D>();
		collider.size = new Vector2(w, h);
		collider.offset = collider.size / 2;
		return obj;
	}

	public static void SetCurrentFromPlayer() {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Vector3 playerPos = player.transform.position;
		foreach (Room room in allRooms) {
			playerPos.z = room.transform.position.z;
			Bounds bounds = new Bounds(room.transform.position + (Vector3) room.bounds.size / 2, room.bounds.size);
			Vector3 extents = bounds.extents;
			bounds.extents = extents;
			if (bounds.Contains(playerPos)) {
				current = room;
				break;
			}
		}
	}

	public void AddMultiRoomObject(MultiRoomObject obj) {
		multiRoomObjects.Add(obj);
	}

}
