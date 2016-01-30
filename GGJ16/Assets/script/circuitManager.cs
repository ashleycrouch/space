using UnityEngine;
using System.Collections;

public class circuitManager : MonoBehaviour {

    public int listSize;
    private int count;
    private circuitButton[] list;

    public object GameObject { get; internal set; }

    // Use this for initialization
    void Start () {
        circuitButton[] list = new circuitButton[listSize];
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addCircuit(circuitButton newcirc)
    {
        count++;

        if (count == listSize)
        {
            for (int x = 0; x < list.Length; x++)
            {
                list[x].disconnect();
                list[x] = null;
                gameObject.SetActive(false);
            }
        }
        else
        {
            list[count - 1] = newcirc;
        }
       
    }

    public void toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
