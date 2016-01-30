using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

	public int width = 1, height = 1;

	void OnDrawGizmos() {
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.color = Color.white;
		Vector3 size = Vector3.right * width + Vector3.up * height;
		Gizmos.DrawWireCube(size / 2, size);
	}
	
}
