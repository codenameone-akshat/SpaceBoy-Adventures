using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public Rigidbody2D rb;
    public bool facingRight;
    public float moveForce = 400f;
    public float maxSpeed = 4f;

    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.localRotation = Quaternion.identity; //to make the tumbling stop.
    }

    private void FixedUpdate()
    {
        if(facingRight && rb.velocity.x <maxSpeed)
            rb.AddForce(Vector2.right * moveForce);
        else if(!facingRight && rb.velocity.x < maxSpeed)
            rb.AddForce(Vector2.left * moveForce);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
        {
            Flip();
        }
    }
}
