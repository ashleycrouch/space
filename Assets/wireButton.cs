using UnityEngine;
using System.Collections;

public class wireButton : MonoBehaviour
{
    public GameObject door;

    // Use this for initialization
    void Start()
    {

    }

    void onGUI()
    {
        //if button is pressed, activate door trigger
        door.GetComponent<BoxCollider2D>().enabled = false;
        //else deactivate door trigger
        door.GetComponent<BoxCollider2D>().enabled = true;
    }
}
