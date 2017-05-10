﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    [HideInInspector] public bool isDead = false;

    public Text scoreText;
    public float moveForce = 400f;
    public float maxSpeed = 7f;
    public float jumpForce = 3000f;
    public Transform groundCheck;

    public bool grounded = false;
    public Animator anim;
    public Rigidbody2D rb;

    int score = 0;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scoreText.text = "0";
    }
    
    // Update is called once per frame
    void Update()
    {

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        transform.localRotation = Quaternion.identity; //to make the tumbling stop.

        if (CrossPlatformInputManager.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }

        scoreText.text = score.ToString();

    }

    private void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb.velocity.x < maxSpeed)
            rb.AddForce(Vector2.right * h * moveForce);
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
        if (jump)
        {
            anim.Play("Jump");
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        if (isDead)
        {
            anim.Play("Die");
            rb.AddForce(new Vector2(-moveForce, jumpForce));
            isDead = false;
            this.enabled = false; //disables the Player Controller Script to avoid movement;
            //call the game over screen after delay                       
        }

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
        if(collision.gameObject.CompareTag("Enemy"))
        {
            isDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            score += 500;   //coins are worth 500 points | common
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Gem"))
        {
            score += 2000;  //gems are worth 5000 points | rare | 1 max in a level
            Destroy(collision.gameObject);
        }
    }
}