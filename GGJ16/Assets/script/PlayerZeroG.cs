using UnityEngine;
using System.Collections;

public class PlayerZeroG : MonoBehaviour {

    public float thrustForce = 10f;
    public float maxSpeed = 8f;

    private Rigidbody2D my_Rigidbody;

	// Use this for initialization
	void Start () {
        my_Rigidbody = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update() {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        if (xInput != 0 && yInput != 0) {
            xInput /= 2;
            yInput /= 2;
        }
        if (xInput < 0) {
            transform.localScale = new Vector2(-1, 1.5f);
        }
        if (xInput > 0) {
            transform.localScale = new Vector2(1, 1.5f);
        }
        transform.localScale = new Vector2(-1, 1.5f);
        if (my_Rigidbody.velocity.magnitude < maxSpeed) {
            my_Rigidbody.AddForce(new Vector2(xInput * thrustForce, yInput * thrustForce));
        }

	}
}
