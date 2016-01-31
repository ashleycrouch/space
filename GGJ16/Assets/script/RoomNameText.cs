using UnityEngine;
using UnityEngine.UI;

public class RoomNameText : MonoBehaviour {

	Text text;

	void Awake() {
		text = GetComponent<Text>();
	}

	void OnEnable() {
		Room.onRoomChange += UpdateText;
	}

	void OnDisable() {
		Room.onRoomChange -= UpdateText;
	}

	void UpdateText(Room current) {
		text.text = current != null ? current.roomName : "";
	}

}