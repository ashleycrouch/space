using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Task : MonoBehaviour, IComparable {

    public bool completed;
    public string room;
    public string description;
    public float timeToComplete;

    private float timer;

    void Start() {
        timer = timeToComplete;
    }

    void Update() {
        if(!completed)
            timer -= Time.deltaTime;

        if(timer < 0) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().kill();
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
        if(time() <= ((Task)x).time()) {
            return 1;
        }
        return -1;
    }
}
