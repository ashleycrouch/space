using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class circuitButton : MonoBehaviour {

    public GameObject thing;
    private circuitManager manager;

    // Use this for initialization
    void Awake()
    {
        manager = GetComponentInParent<circuitManager>();
    }

    public void onClick()
    {
        if (!gameObject.GetComponent<Toggle>().isOn)
        {
            thing.SetActive(true);
            manager.addCircuit(this);
        }
    }

    //method that makes the button disconnect the wires when it's not clicked
    public void disconnect()
    {
        thing.SetActive(false);
        gameObject.GetComponent<Toggle>().isOn = false;
    }
}
