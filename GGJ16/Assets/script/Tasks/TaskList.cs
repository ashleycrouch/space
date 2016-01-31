using UnityEngine;
using System.Collections;

public class TaskList : MonoBehaviour {

    public Task[] tasks;
    public int initialTasks;

    private int counter = 0;
    private TaskManager taskManager;

	void Start () {
        taskManager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
	    for(int i = 0; i < initialTasks; i++) {
            taskManager.AddTask(tasks[i]);
            counter++;
        }
	}
    public void Next() {
        if(counter == tasks.Length) {
            return;
        }
        taskManager.AddTask(tasks[counter]);
        counter++;
    }

}
