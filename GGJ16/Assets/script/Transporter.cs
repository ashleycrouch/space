using UnityEngine;

public class Transporter : MonoBehaviour {

	public Transform destination;
	public bool snapCamera = false;
	public GameObject effect;

	void OnInteract() {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Vector3 destinationPos = destination.position;
		destinationPos.z = player.transform.position.z;
		player.transform.position = destinationPos;
		Room.SetCurrentFromPlayer();
		destinationPos.z = effect.transform.position.z;
		Vector3 pos = transform.position;
		pos.z = destinationPos.z;
		if (effect) {
			Instantiate(effect, pos, Quaternion.identity);
			Instantiate(effect, destinationPos, Quaternion.identity);
		}
		if (snapCamera) {
			CameraRig.main.Snap();
		}
	}

	public void ChangeDestination(Transform destination, string tooltip) {
		this.destination = destination;
		GetComponent<Interactable>().tooltipText = tooltip;
	}

}