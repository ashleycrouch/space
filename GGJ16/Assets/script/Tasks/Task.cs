using UnityEngine;
using System.Collections;

public class Task : MonoBehaviour {

    protected bool completed;

	public virtual void Complete() {
        completed = true;
    }
    public bool isComplete() {
        return completed;
    }
    public virtual Vector2 Location() {
        return transform.position;
    }
}
