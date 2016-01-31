using UnityEngine;
using System.Collections;

public class Sleep : Task {

    private TimeKeeper timeKeeper;
    private TaskList taskList;

    protected override void Start()
    {
        timeKeeper = GameObject.Find("Time").GetComponent<TimeKeeper>();
        taskList = GameObject.Find("TaskList").GetComponent<TaskList>();
        base.Start();
    }

    public override void Complete()
    {
        timeKeeper.NewDay();
        taskList.NewDay();
        GameObject.Find("BreathTimer").SetActive(false);
        base.Complete();
    }
}
