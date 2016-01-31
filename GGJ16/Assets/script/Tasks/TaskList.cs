using UnityEngine;
using System.Collections;

public class TaskList : MonoBehaviour {

    public Task[] tasks;
    public int initialTasks;

    private int counter = 0;
    private ArrayList temp;
    private TaskManager taskManager;

	void Start () {
        temp = new ArrayList();
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
        temp.Add(tasks[counter]);
        temp.TrimToSize();
        counter++;
    }
    public void NewDay() {
        foreach (Task task in tasks) {
            task.Reset();
        }
        foreach (Task task in temp) {
            taskManager.AddTask(task);
            temp.Remove(task);
        }
        GameObject.Find("DailyDialogue").GetComponent<DialogueDays>().NewDay();

    }

}
