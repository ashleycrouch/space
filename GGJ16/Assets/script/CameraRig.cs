using UnityEngine;
using System;

public class CameraRig : MonoBehaviour {

	public static CameraRig main;

	public float smoothing = 15;

	void Awake() {
		main = this;
	}

	void Start() {
		Snap();
	}

	void Update() {
		if (Room.current) {
			Vector3 target = Room.current.bounds.bounds.center;
			target.z = transform.position.z;
			transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * smoothing);
		}
	}

	public void Snap() {
		if (Room.current) {
			Vector3 target = Room.current.bounds.bounds.center;
			target.z = transform.position.z;
			transform.position = target;
		}
	}

}