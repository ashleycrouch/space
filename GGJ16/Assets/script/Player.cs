using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float jumpForce = 500f;
    public float speed = 10f;
    public float boostSpeed = 20f;
    public float boostDistance = 5f;
    public float boostCooldown = 1f;
    public LayerMask groundLayers = LayerMask.NameToLayer("Default");

    private bool boosting = false;
    private bool grounded;
    private bool jumpPressed;
    private bool jumpReleased;
    private bool doubleJumped;
    private bool facingRight = true;
    private float boostTimer = 0f;
    private Vector2 boostTo;
    private Rigidbody2D my_Rigidbody;

	// Use this for initialization
	void Start () {
        my_Rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
    void Update() {
        if (Input.GetAxisRaw("Boost") > 0 && boostTimer <= 0) {
            boosting = true;
            boostTimer = boostCooldown;
            boostTo = new Vector2(facingRight ? transform.position.x + boostDistance : transform.position.x - boostDistance, transform.position.y);
        }
        if (jumpReleased && Input.GetAxisRaw("Vertical") > 0.50f) {
            jumpPressed = true;
            jumpReleased = false;
        }
        if(Input.GetAxisRaw("Vertical") == 0) {
            jumpReleased = true;
        }

        boostTimer -= Time.deltaTime;
    }
	void FixedUpdate () {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        

        if (boosting) {
            Boost();
        } else {
            Move(xInput, yInput);
        }
        GroundCheck();
    }

    private void Move(float x, float y) {
        my_Rigidbody.velocity = new Vector2(speed*x, my_Rigidbody.velocity.y);
        if(my_Rigidbody.velocity.x > 0) {
            transform.localScale = new Vector2(1f,1.5f);
            facingRight = true;
        } else if(my_Rigidbody.velocity.x < 0){
            transform.localScale = new Vector2(-1f, 1.5f);
            facingRight = false;
        }
        if (jumpPressed) {
            if (grounded || !doubleJumped) {
                my_Rigidbody.AddForce(new Vector2(0f, jumpForce));
            }
            if (!grounded) {
                doubleJumped = true;
            }
            jumpPressed = false;
        }
    }

    private void Boost() {
        my_Rigidbody.velocity = new Vector2(facingRight? boostSpeed : -boostSpeed, 0f);
        if((facingRight && transform.position.x > boostTo.x) ||
            (!facingRight && transform.position.x < boostTo.x)) {
            boosting = false;
        }
    }

    private void GroundCheck() {
        Vector2 bottomLeft = new Vector2(transform.position.x - 0.50f, transform.position.y - 0.8f);
        Vector2 bottomRight = new Vector2(transform.position.x + 0.50f, transform.position.y - 0.8f);
        Debug.DrawLine(bottomLeft, bottomRight);
        if (Physics2D.Linecast(bottomLeft, bottomRight, groundLayers)) {
            grounded = true;
            doubleJumped = false;
        } else {
            grounded = false;
        }
    }
}
