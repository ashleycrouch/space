using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.Collections.Generic;

public class TaskManager : MonoBehaviour {

    private ArrayList tasks;
    private GameObject breathTimer;

	// Use this for initialization
	void Start () {
        tasks = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
        string text = "";
        foreach (Task task in tasks) {
            if (!task.isComplete()) {
                text += task.description+"@"+ task.Room() +"-"
                    + ((int)task.time() / 60).ToString().PadLeft(2, '0') +":"
                    + ((int)task.time() % 60).ToString().PadLeft(2, '0')
                    + "\n";
            }

        }
        GetComponent<Text>().text = text;
	}

    public void AddTask(Task a) {
        tasks.Add(a);
        a.startTimer();
        tasks.Sort();
        tasks.TrimToSize();
    }

}
