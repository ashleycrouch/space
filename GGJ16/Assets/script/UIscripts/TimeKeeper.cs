using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeKeeper : MonoBehaviour {

    public float timeMultiplier;
    private float timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer = Time.time*timeMultiplier;
        int minutes = (int)(timer / 60);
        int seconds = (int)timer%60;
        GetComponent<Text>().text = minutes.ToString().PadLeft(2,'0') + ":" + seconds.ToString().PadLeft(2,'0');
	}
}
