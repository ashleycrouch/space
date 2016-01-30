using UnityEngine;
using System.Collections;

public class circuitButton : MonoBehaviour {

    public GameObject thing;
    public bool pressed;
    private circuitManager manager;

    // Use this for initialization
    void Start()
    {
        manager = GetComponentInParent<circuitManager>();
        //manager.enabled = false;
    }

    public void onClick()
    {
        if (!pressed)
        {
            thing.SetActive(true);
            pressed = true;
            manager.addCircuit(this);
        }
    }

    //method that makes the button disconnect the wires when it's not clicked
    public void disconnect()
    {
        thing.SetActive(false);
        pressed = false;
    }
}
