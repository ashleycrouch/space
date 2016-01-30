using UnityEngine;

public class Transporter : MonoBehaviour {

	public Transform destination;
	public bool snapCamera = false;

	void OnInteract() {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Vector3 destinationPos = destination.position;
		destinationPos.z = player.transform.position.z;
		player.transform.position = destinationPos;
		Room.SetCurrentFromPlayer();
		if (snapCamera) {
			CameraRig.main.Snap();
		}
	}

}