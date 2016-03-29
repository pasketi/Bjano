using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private bool grounded = false;
    private bool doubleJump = false;

    private const float jumpPower = 15f;
    private const float moveSpeed = 7.5f;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (bc.IsTouchingLayers()) {
            grounded = true;
            doubleJump = false;
        }
        else grounded = false;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) move(false);
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) move(true);
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) stopMove();

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))) {
            if (grounded)
                jump();
            else if (!grounded && !doubleJump)
            {
                jump();
                doubleJump = true;
            }
        }
	}

    // Movement functions

    private void move(bool right, float speed = moveSpeed) {
        rb.velocity = new Vector2(right ? speed : -speed, rb.velocity.y);
    }

    private void jump (float power = jumpPower) {
        rb.velocity = new Vector2(rb.velocity.x, power);
    }

    private void stopMove () {
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }

}
