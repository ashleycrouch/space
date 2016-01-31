using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Task : MonoBehaviour, IComparable {

    public bool completed;
    public string room;
    public string description;
    public float timeToComplete;

    protected float timer;
    public GameObject breathTimer;

    protected virtual void Start() {
        timer = timeToComplete;
    }

    protected virtual void Update() {
        if(!completed)
            timer -= Time.deltaTime;

        if(timer < 0) {
            breathTimer.SetActive(true);
        }
    }

	public virtual void SetComplete(bool toSet) {
        if (toSet)
            timer = timeToComplete;
        completed = toSet;
    }


    public bool isComplete() {
        return completed;
    }
    public virtual Vector2 Location() {
        return transform.position;
    }
    public virtual string Room() {
        return room;
    }
    public float time() {
        if (timer > 0)
            return timer;
        else
            return 0;
    }
    public int CompareTo(object x){
        Task other = (Task)x;
        if(timeToComplete <= other.timeToComplete) {
            return 1;
        }
        return -1;
    }
}
