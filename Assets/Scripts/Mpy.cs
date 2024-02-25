using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script principal

public class Mpy : MonoBehaviour
{
    //Pour pouvoir bouger le joueur
    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float direction = 0f;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    private Animator playerAnimation;

    //Respawn:
    private Vector3 respawnPoint;
    public GameObject fallDetector;
   
   
   //Pour le score :
    private int score = 0; 
    public Text scoreText;
    public HealthBar healthBar;
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;
        scoreText.text = "Score: " + score;
    }

   
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");

        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
           // Flip
            transform.localScale = new Vector2(0.15f, 0.15f);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            // Flip
            transform.localScale = new Vector2(-0.15f, 0.15f);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }
        // Saut :
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }
        
        //Animation :
        playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);
        //Repsawn
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Respawn
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
        // Checkpoint 
        else if (collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
        // score avec les collectibles 
        else if (collision.tag == "CCup")
        {
            score += 1;
            scoreText.text = "Score: " + score;
            collision.gameObject.SetActive(false);
        }
       
    }
// Bar de vie lorsque le tag "Spike" est touché > - degats (voir script "HealthBar")
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Spike")
        {
            healthBar.Damage(0.002f);
        }
        
    }
}