using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Rigidbody2D rigidBod;
    public float runSpeed;                                                  //runSpeed is the speed set by user
    float velocity;                                                         //velocity is used to change direction of player using runSpeed
    bool click = false;                                                     //click is used to maked sure that each click last 2sec
    float timer = 0;
    float speedMultiplier = 1;
    int clickCounter = 0;
    bool tired = false;
    public float tiredSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rigidBod = GetComponent<Rigidbody2D>();
        velocity = -runSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //handles input; if click=true, start clickCounter; accelerate for 2s from last click
        if ( Input.GetKeyDown("right") )
        {
            click = true;
            handleClickCounter();
        }

        if (click)
            startTimer();

        Debug.Log(clickCounter);

        //accelerationClick = velocity * Input.GetKey("right"); ;       //velocity * true/false

    }

    void FixedUpdate()
    {
        if (timer > 0 && timer < 2)
        {
            rigidBod.velocity = new Vector2( velocity * 2, rigidBod.velocity.y );
        }
        else
        {
            rigidBod.velocity = new Vector2( velocity, rigidBod.velocity.y );
        }

        Flip();
    }


    void Flip()
    {
        //Debug.Log("localscale.x = " + transform.localScale.x);

        if (transform.position.x < -2)
        {
            Vector3 charscale = transform.localScale;
            charscale.x *= -1;                                              //multiplies value of Vector3.x by -1 to change direction
            transform.localScale = charscale;
            velocity = runSpeed;
        }
        else if (transform.position.x > 2)
        {
            Vector3 charscale = transform.localScale;
            charscale.x *= -1;
            transform.localScale = charscale;
            velocity = -runSpeed;
        }

        if (transform.localScale.x == (velocity / runSpeed * 2) )           //If stil going backwards, fix it
        {
            Debug.Log("IT IS GOING BACKWARDSSSSSSSSSSSSS");
            Vector3 charscale = transform.localScale;
            charscale.x *= -1;
            transform.localScale = charscale;
        }
    }

    //makes sure that te interval of acceleration is 2s; resets clickCounter after 2s
    void startTimer()
    {
        timer += Time.deltaTime;
        
        if (timer > 2)
        {
            click = false;
            timer = 0;
            resetClickCounter();
        }
    }

    void resetTimer()
    {
        timer = 0;
    }

    //only let's you accelerate 3 times max; resets timer for every click
    void handleClickCounter()
    {
        if (clickCounter <= 2)
        {
            clickCounter += 1;
            resetTimer();
        }
    }

    void resetClickCounter()
    {
        if (clickCounter == 3)
            tired = true;

        clickCounter = 0;
    }   

}
