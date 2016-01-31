using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float jumpForce = 500f;
    public float speed = 10f;
    public float boostSpeed = 20f;
    public float boostDistance = 5f;
    public float boostCooldown = 1f;
    public LayerMask collisionLayers;

    public bool isWatched = false;
    public bool hasJetPack;
    public bool canHide;

    private Animator animator;
    private bool boosting = false;
    private bool grounded;
    private bool jumpPressed;
    private bool jumpReleased;
    private bool doubleJumped;
    private bool facingRight = true;
    //private bool hiding;
    private bool idle;
    private float boostTimer = 0f;
    private Vector2 boostTo;
    protected Rigidbody2D my_Rigidbody;

	// Use this for initialization
	void Start () {
        my_Rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
    protected virtual void Update() {
        isWatched = false;

        if (Input.GetAxisRaw("Boost") > 0 && boostTimer <= 0 && hasJetPack) {
            boosting = true;
            boostTimer = boostCooldown;
            if (idle) {
                boostTo = new Vector2(this.transform.position.x, this.transform.position.y + boostDistance/1.5f);
            } else {
                boostTo = new Vector2(facingRight ? transform.position.x + boostDistance : transform.position.x - boostDistance, transform.position.y);
            }
        }
        if (jumpReleased && Input.GetAxisRaw("Vertical") > 0.50f) {
            jumpPressed = true;
            jumpReleased = false;
        }
        if(Input.GetAxisRaw("Vertical") <= 0) {
            jumpReleased = true;
        }

        boostTimer -= Time.deltaTime;

    }
    protected virtual void FixedUpdate () {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        //Debug.Log(yInput);

        GetComponent<BoxCollider2D>().enabled = true;
        my_Rigidbody.isKinematic = false;

        if (boosting) {

            Boost(); 
        } else if (canHide && (yInput == -1)) {
            //will play hiding animation
            GetComponent<BoxCollider2D>().enabled = false;
            //hiding = true;
            animator.SetInteger("AnimState", 6);
            my_Rigidbody.isKinematic = true;
        }else{
            Move(xInput, yInput);
        }
        GroundCheck();
    }

    private void Move(float x, float y) {
        my_Rigidbody.velocity = new Vector2(speed*x, my_Rigidbody.velocity.y);
        //hiding = false;
        idle = false;
        if(x > 0) {
            transform.localScale = new Vector2(1f,1.5f);
            facingRight = true;
            animator.SetInteger("AnimState", 1);
        } else if(x < 0){
            transform.localScale = new Vector2(-1f, 1.5f);
            facingRight = false;
            animator.SetInteger("AnimState", 1);
        } else if(my_Rigidbody.velocity.magnitude == 0f) {
            idle = true;
            animator.SetInteger("AnimState", 0);

        }
        if (!grounded) {
            animator.SetInteger("AnimState", 3);
        }

        if (jumpPressed) {
            if (grounded || (!doubleJumped && hasJetPack)) {
                my_Rigidbody.AddForce(new Vector2(0f, jumpForce));
                if (!grounded) {
                    doubleJumped = true;
                    animator.SetInteger("AnimState", 4);
                }
            }

            jumpPressed = false;
        }
    }

    private void Boost() {
        CheckBoostLimit();
        if (idle) {
            my_Rigidbody.velocity = new Vector2(0f, boostSpeed);
            animator.SetInteger("AnimState", 5);
        } else {
            animator.SetInteger("AnimState", 2);
            my_Rigidbody.velocity = new Vector2(facingRight ? boostSpeed : -boostSpeed, 0f);
        }
        if((idle && transform.position.y > boostTo.y)||
            (facingRight && transform.position.x > boostTo.x) ||
            (!facingRight && transform.position.x < boostTo.x)) {
            boosting = false;
            my_Rigidbody.velocity = new Vector2(my_Rigidbody.velocity.x, 0f);
        }
    }

    private void GroundCheck() {
        Vector2 bottomLeft = new Vector2(transform.position.x - 0.50f, transform.position.y - 0.8f);
        Vector2 bottomRight = new Vector2(transform.position.x + 0.50f, transform.position.y - 0.8f);
        Debug.DrawLine(bottomLeft, bottomRight);
        if (Physics2D.Linecast(bottomLeft, bottomRight, collisionLayers)) {
            grounded = true;
            doubleJumped = false;
        } else {
            grounded = false;
        }
    }

    public void kill() {
        Debug.Log("YOU PASSED OUT");
        Vector3 newpos = GameObject.FindGameObjectWithTag("Bed").transform.position;
        newpos.z = transform.position.z;
        transform.position = newpos;
        Room.SetCurrentFromPlayer();
        CameraRig.main.Snap();

        GameObject.FindGameObjectsWithTag("Bed");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Hidable") {
            canHide = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Hidable") {
            canHide = false;
        }
    }
    void CheckBoostLimit() {
        Vector2 end;
        if (idle) {
            end = new Vector2(transform.position.x, transform.position.y+0.75f);
        } else {
            end = new Vector2(transform.position.x + (facingRight ? 0.75f : -0.75f), transform.position.y);
        }
        if(Physics2D.Linecast(transform.position, end, collisionLayers)) {
            boosting = false;
        }
        Debug.DrawLine(transform.position, end);

    }
}
