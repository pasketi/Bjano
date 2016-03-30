using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private bool grounded = false;
    private bool doubleJump = false;
    private bool noControl = false;
    private bool dirRight = true;
    private Vector3 playerScale;

    private const float jumpPower = 15f;
    private const float moveSpeed = 7.5f;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        playerScale = gameObject.transform.localScale;
	}

    // Update is called once per frame
    void Update()
    {
        if (bc.IsTouchingLayers())
        {
            grounded = true;
            doubleJump = false;
        }
        else grounded = false;

        if (!noControl)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) move(false);
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) move(true);
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) stopMove();

            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
            {
                if (grounded)
                    jump();
                else if (!grounded && !doubleJump)
                {
                    jump();
                    doubleJump = true;
                }
            }
        }

        if (grounded && !noControl) noControl = false;
	}

    // Movement functions

    private void move(bool right, float speed = moveSpeed) {
        dirRight = right;
        rb.velocity = new Vector2(right ? speed : -speed, rb.velocity.y);
        gameObject.transform.localScale = new Vector3(dirRight ? playerScale.x : -playerScale.x, playerScale.y, playerScale.z);
    }

    private void jump (float power = jumpPower) {
        rb.velocity = new Vector2(rb.velocity.x, power);
    }

    private void stopMove () {
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    private void takeHit ()
    {
        rb.velocity = new Vector2((dirRight ? -moveSpeed : moveSpeed) / 2f , jumpPower / 2f);
        noControl = true;
        grounded = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 11 || other.gameObject.layer == 10) takeHit();
    }
}
