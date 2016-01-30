using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextTrigger : MonoBehaviour {

    public TextAsset textAsset;

    private GameObject dialogue;
    

	// Use this for initialization
	void Start () {
        dialogue = GameObject.Find("Dialogue");
	}
	

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            //The following is called whenever you want to send a textasset to the dialogue script
            dialogue.GetComponent<TextDialogue>().sendText(textAsset);
        }
    }
}
