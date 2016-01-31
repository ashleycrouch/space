using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class logScript : MonoBehaviour {

    public TextAsset log;
    private Text display;

	// Use this for initialization
	void Awake () {
        Time.timeScale = 0f;
        display = gameObject.GetComponentInChildren<Text>();

		display.text = log.text;
	}
	
    void onClick()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
