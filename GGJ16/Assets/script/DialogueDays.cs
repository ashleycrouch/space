using UnityEngine;
using System.Collections;

public class DialogueDays : MonoBehaviour {

    public TextDialogue textD;
    public TextAsset[] texts;
    public int counter = 0;

	// Use this for initialization
	void Start () {
        textD.sendText(texts[0]);
	}
    public void NewDay() {
        counter++;
        if(counter != texts.Length - 1) {
            textD.sendText(texts[counter]);
        }
    }
}
