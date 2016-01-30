using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour {

	public Text tooltip;
	public Image progressBar;

	bool interacting;
	float progress;

	void Update() {
		//handle tooltip and position
		tooltip.text = "";
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = transform.position.z;
		transform.position = pos;
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		Interactable interactable = null;
		if (hit.collider) {
			interactable = hit.collider.GetComponent<Interactable>();
			if (interactable && interactable.InRange()) {
				tooltip.text = interactable.tooltipText;
			}
		}
		//handle interaction progress
		if (interactable && interactable.InRange()) {
			if (!interacting && Input.GetMouseButtonDown(0)) {
				interacting = true;
				//handle instant interactions
				if (interactable.interactTime == 0) {
					interactable.Interact();
					interacting = false;
				}
			}
			if (interacting && interactable.interactTime > 0 && Input.GetMouseButton(0)) {
				progress += Time.deltaTime / interactable.interactTime;
				if (progress >= 1) {
					interactable.Interact();
					interacting = false;
					progress = 0;
				}
			}
			else {
				progress = 0;
			}
		}
		else {
			interacting = false;
			progress = 0;
		}
		progressBar.fillAmount = progress;
	}


}