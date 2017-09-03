using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    [HideInInspector] public bool isDead = false;

    public Text scoreText;
    public float moveForce = 500f;
    public float maxSpeed = 7f;
    public float jumpForce = 3500f;
    public Transform groundCheck;

    public bool grounded = false;
    public Animator anim;
    public Rigidbody2D rb;
    public AudioClip[] aud;

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

        rb.AddForce(new Vector2(1, 0) * rb.velocity.x * -15);   //to counter the slipping force -15 is the multiplier

        if (h * rb.velocity.x < maxSpeed)
            rb.AddForce(Vector2.right * h * moveForce); //if not at max, then keep adding force
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y); //if greater then same direction and max speed (direction by sign)

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
        if (jump)
        {
            anim.Play("Jump");
            AudioSource.PlayClipAtPoint(aud[0], rb.transform.position);
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        if (isDead)
        {
            anim.Play("Die");
            AudioSource.PlayClipAtPoint(aud[2], rb.transform.position);
            rb.AddForce(new Vector2(-moveForce, jumpForce));
            isDead = false;
            this.enabled = false; //disables the Player Controller Script to avoid movement;
            //TODO call the score screen after delay 
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

        if (collision.gameObject.CompareTag("Water"))
        {
            isDead = true;
            //TODO SceneManager.LoadScene("GameOverMenu")
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            score += 500;   //coins are worth 500 points | common
            AudioSource.PlayClipAtPoint(aud[1], this.transform.position);
            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.CompareTag("Gem"))
        {
            score += 2000;  //gems are worth 5000 points | rare | 1 max in a level
            AudioSource.PlayClipAtPoint(aud[1], this.transform.position);
            Destroy(collision.gameObject);
        }
    }
}