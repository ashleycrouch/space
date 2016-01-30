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
            dialogue.GetComponent<Text>().text = "";
            dialogue.GetComponent<TextDialogue>().textAsset = textAsset;
        }
    }
}
