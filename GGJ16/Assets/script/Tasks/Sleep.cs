using UnityEngine;
using System.Collections;

public class Sleep : Task {

    private TimeKeeper timeKeeper;
    private TaskList taskList;
    private TaskManager taskManager;

    protected override void Start()
    {
        timeKeeper = GameObject.Find("Time").GetComponent<TimeKeeper>();
        taskList = GameObject.Find("TaskList").GetComponent<TaskList>();
        taskManager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        base.Start();
    }

    public override void Complete()
    {
        timeKeeper.NewDay();
        taskList.NewDay();
        taskManager.ResetCompletion();
        base.Complete();
    }
}
