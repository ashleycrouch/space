using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextDialogue : MonoBehaviour {

    public TextAsset textAsset;
    private char[] chars;
    public float timeToNextLetter = 0.1f;
    public float waitForNextLine = 3f;

    private float timer = 0f;
    private int counter;
    private int max;

	// Use this for initialization
	void Awake () {
        chars = textAsset.ToString().ToCharArray();
        max = chars.Length;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (counter != max && timer > timeToNextLetter) {

            if (chars[counter].Equals('\n') || counter == max-1) {
                nextPage();
                return;
            }
            counter++;

            timer = 0f;
            this.GetComponent<Text>().text += chars[counter];
            
        }
	}

    void nextPage() {
        if (timer >= waitForNextLine) {
            this.GetComponent<Text>().text = "";
            counter++;
        }
    }
}
