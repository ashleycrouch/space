using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BreathTimer : MonoBehaviour {

    public float breathTime = 30f;

    public GameObject player;
    private float timer = 0f;


	void OnEnable () {
        timer = breathTime;
        player = GameObject.FindWithTag("Player");
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
