using UnityEngine;
using System.Collections;

public class panelScript : MonoBehaviour {

    private wireManager manager;

    // Use this for initialization
    void Start () {
        manager = GameObject.FindObjectOfType<wireManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClick()
    {
        manager.enabled = true;
    }
}
