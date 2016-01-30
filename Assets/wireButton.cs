using UnityEngine;
using System.Collections;

public class wireButton : MonoBehaviour
{
    public GameObject door;
    public bool pressed;
    private wireManager manager;

    // Use this for initialization
    void Start()
    {
        manager = GetComponentInParent<wireManager>();
    }

    public void onClick()
    {
        if (!pressed)
        {
        door.GetComponent<BoxCollider2D>().enabled = false;
        pressed = true;
        manager.newCurrent(this);
        }
    }

    //method that makes the button disconnect the wires when it's not clicked
    public void disconnect()
    {
        door.GetComponent<BoxCollider2D>().enabled = true;
        pressed = false;
    }

}
