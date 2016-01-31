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
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            canvas.gameObject.SetActive(false);
        }
	}

    public void onClick()
    {
        manager.gameObject.SetActive(true);
    }
}
