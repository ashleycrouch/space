using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class circuitButton : MonoBehaviour {

    public UnityEvent select;
    public UnityEvent deselect;
    private circuitManager manager;

    // Use this for initialization
    void Awake()
    {
        manager = GetComponentInParent<circuitManager>();
    }

    public void onClick()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            select.Invoke();
            manager.addCircuit(this);
        }
        else if(manager != null)
        {
            deselect.Invoke();
            manager.deleteCircuit(this);
        }
    }

    //method that makes the button disconnect the wires when it's not clicked
    public void disconnect()
    {
        gameObject.GetComponent<Toggle>().isOn = false;
        deselect.Invoke();
    }

    public void connect()
    {
        gameObject.GetComponent<Toggle>().isOn = true;
        select.Invoke();
    }
}
