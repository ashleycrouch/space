using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

	public Text text;

	void Update() {
		text.text = "";
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Debug.Log(pos);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		if (hit.collider) {
			Interactable interact = hit.collider.GetComponent<Interactable>();
			if (interact && interact.InRange()) {
				text.text = interact.tooltip;
			}
		}
	}


}