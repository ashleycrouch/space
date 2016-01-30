using UnityEngine;
using System.Collections;

public class wireButton : MonoBehaviour
{
    public GameObject door;
    public bool pressed;

    // Use this for initialization
    void Start()
    {

    }

    public void onClick()
    {
        door.GetComponent<BoxCollider2D>().enabled = false;
        pressed = true;
    }

    //method that makes the button disconnect the wires when it's not clicked
    public void disconnect()
    {
        door.GetComponent<BoxCollider2D>().enabled = true;
        pressed = false;
    }

}
