using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddle : MonoBehaviour {

    public float speed = 2;

    private Vector3 downMost;
    private Vector3 upMost;
    private bool paddlePause;
    private float minY, maxY;
    private float halfHeight;

    //allowing scripts to talk to one another
    // Capital ball is the name of script
    //lowercase ball is the var name
    private Ball ball;
    

	// Use this for initialization
	void Start () {
        //go through the hierachy, find the Ball object and store it in the var ball
        ball = GameObject.FindObjectOfType<Ball>();

        halfHeight = GetComponent<SpriteRenderer>().sprite.bounds.extents.x;

        //restrict the area that the paddle can move up and down to
        float distance = transform.position.y - Camera.main.transform.position.y;
        downMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        upMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));

        minY = downMost.y + halfHeight;
        maxY = upMost.y - halfHeight; 
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.tag == "Player")
        {
            MovePlayerPaddle();
        }
       else
        { //! = if paddle pause is set to FALSE move computer paddle
            // no ! = if paddle pause is set to TRUE move computer paddle
            if (!paddlePause)
            {

                MoveComputerPaddle();
            }
        }
		
	}

    void MoveComputerPaddle()
    {
        float paddlePosY = transform.position.y; 

        if(paddlePosY >= ball.ballPosY && paddlePosY <= ball.ballPosY + 0.15f)
        {
            paddlePause = true;
            Invoke("PaddleDelay", 0.05f);
            return;
        }
        else if (paddlePosY <= ball.ballPosY && paddlePosY >= ball.ballPosY + 0.15f)
        {
            paddlePause = true;
            Invoke("PaddleDelay", 0.05f);
            return; 
        }

        if (paddlePosY < ball.ballPosY)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        //constrain the computer to the playing field
        float restrictY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, restrictY, transform.position.z);

    }

    void MovePlayerPaddle()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MovePlayerUp();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MovePlayerDown();
        }
        // to tell unity to keep the paddle in the play space
        float restrictY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, restrictY, transform.position.z);

    }

    void PaddleDelay()
    {
        paddlePause = false;
    }

    void MovePlayerUp()
    {
        // telling unity to look at the transform  variables in unity
        // looking at the position section, this will allow us to manipulate w/o changing the values manually
        // time.deltatime is to ensure the paddle moves at the same speed no matter how good our computer operates

        transform.position += Vector3.up * speed * Time.deltaTime; 
    }
    void MovePlayerDown()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
