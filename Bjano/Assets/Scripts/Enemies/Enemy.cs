using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private Rigidbody2D rb;
    private BoxCollider2D bc;

    private bool grounded = false;

    private bool dirRight = false;

    public bool activateAI = false;

    private float timer = 0f;

    private const float jumpPower = 15f;
    private const float moveSpeed = 4f;
    private Vector3 spriteScale;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        spriteScale = gameObject.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
	    if (activateAI)
        {
            if (grounded)
            {
                rb.velocity = new Vector2((dirRight ? -moveSpeed : moveSpeed), rb.velocity.y);
                gameObject.transform.localScale = new Vector3(dirRight ? spriteScale.x : -spriteScale.x, spriteScale.y, spriteScale.z);
            }
            if (timer > 1f)
            {
                timer = 0f;
                dirRight = !dirRight;
            }
            timer += Time.deltaTime;
        }
	}

    private void takeHit()
    {
        rb.velocity = new Vector2((dirRight ? moveSpeed : -moveSpeed) / 2f, jumpPower / 2f);
        grounded = false;
        Debug.Log("Enemy Hit");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9) takeHit();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 12) grounded = true;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 12) grounded = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 12) grounded = false;
    }
}
