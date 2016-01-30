using UnityEngine;
using System.Collections;

public class panelScript : MonoBehaviour {

    private circuitManager manager;
    private Canvas canvas;

    // Use this for initialization
    void Start () {
        manager = GameObject.FindObjectOfType<circuitManager>();

        manager.gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClick()
    {
        manager.gameObject.SetActive(true);
    }
}
