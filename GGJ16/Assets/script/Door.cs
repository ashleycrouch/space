using UnityEngine;

public class Door : MonoBehaviour {

	public float distance = 1;
	public float moveTime = 1;

	float t = 0;

	float dir = 0;

	bool _open;

	Vector3 neutralPosition;

	public bool startOpen = true;

	public bool open {
		get {
			return _open;
		}
		set {
			if (value) {
				Open();
			}
			else {
				Close();
			}
		}
	}

	void Awake() {
		neutralPosition = transform.localPosition;
		if (startOpen) {
			t = 1;
			transform.localPosition = neutralPosition + Vector3.up * distance * t;
		}
	}

	void Update() {
		t += Time.deltaTime / moveTime * dir;
		if (dir > 0) {
			if (t >= 1) {
				dir = 0;
				t = 1;
			}
		}
		else {
			if (t <= 0) {
				dir = 0;
				t = 0;
			}
		}
		transform.localPosition = neutralPosition + Vector3.up * distance * t;
	}

	public void Open() {
		_open = true;
		dir = 1;
	}

	public void Close() {
		_open = false;
		dir = -1;
	}

	public void Toggle() {
		open = !open;
	}

}