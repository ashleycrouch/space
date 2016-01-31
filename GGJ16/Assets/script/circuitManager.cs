using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class circuitManager : MonoBehaviour {

    public int listSize;
    public int count;
    private circuitButton[] list;

    // Use this for initialization
    void Awake() {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addCircuit(circuitButton newcirc)
    {
        if (count == listSize)
        {
            list = GetComponentsInChildren<circuitButton>();
            for (int x = 0; x < list.Length; x++)
            {
                if (list[x].GetComponent<Toggle>().isOn)
                {
                    list[x].disconnect();
                }
            }
            count = 0;
            gameObject.SetActive(false);
        }
        else
        {
            count++;
        }
    }

    public void deleteCircuit(circuitButton circ)
    {
        circ.disconnect();
        count--;
    }

    public void toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
