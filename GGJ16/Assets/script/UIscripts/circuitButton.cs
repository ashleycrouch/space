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
        Debug.Log(gameObject.GetComponent<Toggle>().isOn);
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            thing.SetActive(true);
            manager.addCircuit(this);
        }
        else if(manager != null)
        {
            thing.SetActive(false);
            manager.deleteCircuit(this);
        }
    }

    //method that makes the button disconnect the wires when it's not clicked
    public void disconnect()
    {
        gameObject.GetComponent<Toggle>().isOn = false;
        thing.SetActive(false);
    }
}
