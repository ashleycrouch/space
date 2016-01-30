using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

	static Room _current;
	public static Room current {
		get { return _current; }
		set {
			if (_current) {_current.gameObject.SetActive(false); }
			_current = value;
			if (_current) { _current.gameObject.SetActive(true); }
		}
	}

	public const int WIDTH = 16;
	public const int HEIGHT = 12;

	public BoxCollider2D bounds { get; private set; }

	void Awake() {
		bounds = gameObject.AddComponent<BoxCollider2D>();
		bounds.size = new Vector2(WIDTH, HEIGHT);
		bounds.offset = bounds.size / 2;
		
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player && bounds.bounds.Contains(new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z))) {
			current = this;
		}
	}

	void Start() {
		int borderWidth = 1;
		//left wall
		CreateBorderWall(-borderWidth, 0, borderWidth, HEIGHT);
		//right wall
		CreateBorderWall(WIDTH, 0, borderWidth, HEIGHT);
		//bottom wall
		CreateBorderWall(-borderWidth, -borderWidth, WIDTH + borderWidth * 2, borderWidth);
		//top wall
		CreateBorderWall(-borderWidth, HEIGHT, WIDTH + borderWidth * 2, borderWidth);
		if (current != this) {
			gameObject.SetActive(false);
		}
	}

	void OnDrawGizmos() {
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.color = current == this ? Color.blue : Color.white;
		Vector3 size = new Vector3(WIDTH, HEIGHT, 0);
		Gizmos.DrawWireCube(size / 2, size);
	}

	GameObject CreateBorderWall(int x, int y, int w, int h) {
		GameObject obj = new GameObject("border wall");
		obj.transform.parent = transform;
		obj.transform.localPosition = new Vector3(x, y, 0);
		BoxCollider2D collider = obj.AddComponent<BoxCollider2D>();
		collider.size = new Vector2(w, h);
		collider.offset = collider.size / 2;
		return obj;
	}

}
