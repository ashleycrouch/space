using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextDialogue : MonoBehaviour {

    private TextAsset textAsset;
    private char[] chars;
    public float timeToNextLetter = 0.1f;
    public float waitForNextLine = 3f;
    public AudioSource audioSource;
    public AudioClip typeSound;

    private float timer = 0f;
    private int counter;
    private int max;
    private bool lastLetter = false;

	void Update () {
        timer += Time.deltaTime;

        if (counter != max && timer > timeToNextLetter) {

            if (chars[counter].Equals('\n') || counter == max-1) {
                if (counter == max - 1 && !lastLetter) {
                    this.GetComponent<Text>().text += chars[counter];
                    lastLetter = true;
                }
                nextPage();
                return;
            }
            

            timer = 0f;
            this.GetComponent<Text>().text += chars[counter];
            counter++;
            audioSource.pitch = Random.Range(1f, 1.5f);
            audioSource.Play();

        } else {
            audioSource.Stop();
        }
    }

    void nextPage() {
        if (timer >= waitForNextLine) {
            this.GetComponent<Text>().text = "";
            counter++;
        }
    }

    public void sendText(TextAsset incoming) {
        GetComponent<Text>().text = "";
        textAsset = incoming;
        chars = textAsset.ToString().ToCharArray();
        max = chars.Length;
        counter = 0;
        lastLetter = false;
    }
}
