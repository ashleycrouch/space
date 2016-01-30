using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

	public string tooltipText = "<interactable>";
	public float range = 5;
	public float interactTime = 3;

	public UnityEvent onInteract;

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, range);
	}

	public bool InRange() {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		return Vector2.Distance(player.transform.position, transform.position) <= range;
	}

	public void Interact() {
		SendMessage("OnInteract", SendMessageOptions.DontRequireReceiver);
	}

	void OnInteract() {
		onInteract.Invoke();
	}

}