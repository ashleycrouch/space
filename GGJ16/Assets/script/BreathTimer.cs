using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BreathTimer : MonoBehaviour {

    public float breathTime = 30f;

    private GameObject player;
    private float timer = 0f;

	// Use this for initialization
	void OnEnable () {
        timer = breathTime;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer >= 0) {
            GetComponent<Text>().text = ((int)timer % 60).ToString();
        } else {
            player.GetComponent<Player>().kill();
        }
    }
}
