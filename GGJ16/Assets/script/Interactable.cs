using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

	public string tooltip = "<interactable>";
	public float range = 5;

	public UnityEvent OnInteract;

	void OnMouseDown() {
		if (InRange()) {
			OnInteract.Invoke();
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, range);
	}

	public bool InRange() {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		return Vector2.Distance(player.transform.position, transform.position) <= range;
	}

}