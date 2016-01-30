using UnityEngine;
using System.Collections;

public class breathTimer : MonoBehaviour {

    //this is enabled when a task is not completed by its time
    public float timeLeft;

    void Awake()
    {
        timeLeft = 30;
    }
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;

        if(timeLeft < 0)
        {
            Application.LoadLevel(Application.loadedLevel);
            //needs to also activate the AI's different beginning text and a task is added to the list
        }
	
	}
}
