using UnityEngine;
using System;

public class CameraRig : MonoBehaviour {

	public string targetTag = "Player";

	Transform target;

	void Start() {
		UpdateTarget();
	}

	void Update()
	{

	}

	public void UpdateTarget() {
		target = GameObject.FindGameObjectWithTag(targetTag).transform;
	}


}