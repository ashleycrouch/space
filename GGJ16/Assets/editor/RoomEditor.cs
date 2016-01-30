using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Room))]
public class RoomEditor : Editor {

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		if (GUILayout.Button("Make Current")) {
			Room.current = (Room) target;
		}
	}

}