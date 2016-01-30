﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.Collections.Generic;

public class TaskManager : MonoBehaviour {

    public Task[] initialTasks;
    private ArrayList tasks;

	// Use this for initialization
	void Start () {
        tasks = new ArrayList();
        foreach(Task task in initialTasks) {
            AddTask(task);
        }
	}
	
	// Update is called once per frame
	void Update () {
        string text = "";
        Debug.Log(tasks.Capacity);
        foreach (Task task in tasks) {
            if (!task.isComplete()) {
                text += (((int)task.time() / 60) > 0 ? (((int)task.time() / 60).ToString()+":"): "" ) + ((int)task.time() % 60).ToString() +" "+task.description+" "+ task.Room() +"\n";
            }

        }
        GetComponent<Text>().text = text;
	}

    public void AddTask(Task a) {
        tasks.Add(a);
        tasks.Sort();
        tasks.TrimToSize();
    }
    public void ResetCompletion() {
        foreach(Task task in tasks) {
            task.SetComplete(false);
        }
    }
}
