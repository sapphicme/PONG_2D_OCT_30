using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody2D body;
    private float forceX, forceY;
    //to allow ball script to talk to scoremanager script
    private ScoreManager scoreManager;
    //where ball is going to initialize
    private bool towardsPlayer;
    private AudioSource audioSource;
    public AudioClip scoreSound, wallSound, paddleSound;

    //to know where the ball is at all times
    [HideInInspector]  public float ballPosY;

	// Use this for initialization
	void Start () {

        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        // declaring initial values
        int roll = Random.Range(0, 2);
        forceX = 5f;

        if (roll == 0)
        {
            towardsPlayer = false;
        }
        else
        {
            towardsPlayer = true;
        }

        forceY = Random.Range(-2,2); 
        MoveBall();
	}

	//to move ball
    void MoveBall()
    {
        if (towardsPlayer == true)
        {
            body.velocity = new Vector2(-forceX, forceY);
        }
        else
        {
            body.velocity = new Vector2(forceX, forceY);
        }

    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        // to create variation when ball hits each collider
        if (collider.gameObject.name == "Top")

        { // ball will bounce in an upward direction
            body.velocity += new Vector2(0, 1).normalized;
        }
        else if (collider.gameObject.name == "Bottom")

        { // ball will bounce in a downward direction
            body.velocity += new Vector2(0, -1).normalized;
        }
        else if (collider.gameObject.name == "TopWall")
        {
            body.velocity += new Vector2(0, -0.000000000000000000001f).normalized;
        }
        else if (collider.gameObject.name == "BottomWall")
        {
            body.velocity += new Vector2(0, 0.000000000000000000001f).normalized;
        }

        if (audioSource.clip != paddleSound)
        {
            audioSource.clip = paddleSound;
        }

        if (collider.gameObject.tag == "topWall")
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = wallSound;
            audioSource.Play();
        }
        if (collider.gameObject.tag == "bottomWall")
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = wallSound;
            audioSource.Play();
        }

        audioSource.Play();
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
       
            if (trigger.name == "RightWall")
        {
            scoreManager.IncreasePlayerScore();
            towardsPlayer = true;
            audioSource.clip = scoreSound;
            audioSource.Play();
        }
        else if (trigger.name == "LeftWall")
        {
            scoreManager.IncreaseComputerScore();
            towardsPlayer = false;
            audioSource.clip = scoreSound;
            audioSource.Play();
        }
            
        ResetBall();
    }

    //to reset ball to 0
    void ResetBall()
    {
        transform.position = new Vector2(0, 0);
        //when ball resets it also resets its initial velocity
        MoveBall();
    }

    // Update is called once per frame
    void Update () {

        ballPosY = transform.position.y;

	}
}
