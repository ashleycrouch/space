using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

	public static Room current;
	public const int WIDTH = 24;
	public const int HEIGHT = 16;

	public BoxCollider2D bounds { get; private set; }

	void Awake()	{
		bounds = gameObject.AddComponent<BoxCollider2D>();
		bounds.size = new Vector2(WIDTH, HEIGHT);
		bounds.offset = bounds.size / 2;
	}

	void OnDrawGizmos() {
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.color = current == this ? Color.blue : Color.white;
		Vector3 size = new Vector3(WIDTH, HEIGHT, 0);
		Gizmos.DrawWireCube(size / 2, size);
	}

}
