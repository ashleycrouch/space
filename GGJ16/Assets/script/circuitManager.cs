using UnityEngine;
using System.Collections;

public class circuitManager : MonoBehaviour {

    public int listSize;
    public circuitButton[] list;

	// Use this for initialization
	void Start () {
        circuitButton[] list = new circuitButton[listSize];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addCircuit(circuitButton newcirc)
    {
        int check = 0;
        for(int x = 0; x < list.Length; x++)
        {
            if(list[x] != null)
            {
                check++;
            }
        }

        if(check == listSize)
        {
            for(int x = 0; x < list.Length; x++)
            {
                list[x].disconnect();
                list[x] = null;
            }
        }
        else
        {
            list[check - 1] = newcirc;
        }
    }

}
