using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Initializations
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    [HideInInspector] public bool isDead = false;

    public Text HUDScore;
    public Text UIScore;
    public Text UIHighScore;
    public GameObject ScorePanel;

    public float moveForce = 600f;
    public float maxSpeed = 5f;
    public float jumpForce = 1250f;
    public Transform groundCheck;

    public bool grounded = false;
    public Animator anim;
    public Rigidbody2D rb;
    public AudioClip[] aud;

    int score = 0;
    int highScore = 0;
    string highScoreSave = "";
    #endregion Initializations


    void Awake()
    {
        //---------Getting Components--------------
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        InitializeScore();  //initializes the score
    }

    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  //check if player is grounded

        transform.localRotation = Quaternion.identity; //to make the tumbling stop.

        if (CrossPlatformInputManager.GetButtonDown("Jump") && grounded)
            jump = true;

        HUDScore.text = score.ToString();  //to set the HUD score

        if(isDead)
            DisplayFinalScore();
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
            JumpScenario();

        if (isDead)
            DeathScenario();
    }

    void Flip()     //flip the player according to the player movement direction
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isDead = true;
        }

        if (collision.gameObject.CompareTag("Water"))
        {
            isDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            score += 500;   //coins are worth 500 points | common
            AudioSource.PlayClipAtPoint(aud[1], this.transform.position, 1.0f);
            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.CompareTag("Gem"))
        {
            score += 2000;  //gems are worth 5000 points | rare | 1 max in a level
            AudioSource.PlayClipAtPoint(aud[1], this.transform.position, 1.0f);
            Destroy(collision.gameObject);
        }
    }

    void InitializeScore()  //to initialize the score
    {
        //-------------------------------Getting Highscore and Setting Initial Score and Highscore Values------------------------------------
        HUDScore.text = "0";
        UIScore.text = "0";
        highScoreSave = "Highscore" + SceneManager.GetActiveScene().buildIndex.ToString();   //saves score level by level. EX: Highscore1 for level 1 etc etc
        highScore = PlayerPrefs.GetInt(highScoreSave, 0);   //loading the highscore if available, else 0
        UIHighScore.text = highScore.ToString();
    }  

    [HideInInspector] public void DisplayFinalScore()    // to display final score
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(highScoreSave, score);
            PlayerPrefs.Save();
        }
        ScorePanel.SetActive(true); //setting the pop up active

        UIScore.text = score.ToString();
        UIHighScore.text = highScore.ToString();
    }   

    void JumpScenario()     //things to do when we jump
    {
        anim.Play("Jump");
        AudioSource.PlayClipAtPoint(aud[0], rb.transform.position);
        rb.AddForce(new Vector2(0f, jumpForce));
        jump = false;
    }  
        
    void DeathScenario()    //things to do when we die
    {
        anim.Play("Die");
        AudioSource.PlayClipAtPoint(aud[2], rb.transform.position);
        rb.AddForce(new Vector2(-moveForce, jumpForce));
        isDead = false;
        this.enabled = false; //disables the Player Controller Script to avoid movement;
    }  
}