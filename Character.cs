using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody2D rigidBod;
    public Animator anim;
    public float runSpeed;                                                  //runSpeed is the speed set by user
    float velocity;                                                         //velocity is used to change direction of player using runSpeed
    bool click = false;                                                     //click is used to maked sure that each click last 2sec
    float timer = 0;
    float speedMultiplier = 1;
    int clickCounter = 0;
    bool tired = false;
    float tiredTimer = 0;
    public float tiredSpeed;

    public BrickLeft[] left;

    // Start is called before the first frame update
    void Start()
    {
        rigidBod = GetComponent<Rigidbody2D>();
        velocity = -runSpeed;
        left = FindObjectsOfType<BrickLeft>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //handles input; if click=true, start clickCounter; accelerate for 2s from last click
        if (Input.GetKey("right"))
        {
            speedMultiplier = 3.4f;
        }
        else 
        {
            speedMultiplier = 1;
        }



        //accelerationClick = velocity * Input.GetKey("right"); ;       //velocity * true/false

    }

    void FixedUpdate()
    {
        rigidBod.velocity = new Vector2(velocity * speedMultiplier, 0);

        Flip();
    }

    void Flip()
    {
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
            resetTimer();
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
            speedMultiplier *= 1.3f;
            clickCounter += 1;
            resetTimer();
        }
    }

    void resetClickCounter()
    {
        if (clickCounter == 3)
        {
            tired = true;
            speedMultiplier = 0f;
        }

        clickCounter = 0;
    }   

    void startTiredTimer()
    {
        tiredTimer += Time.deltaTime;

        if (tiredTimer > 2)
        {
            Debug.Log("Inside tiredTimer >2");
            tired = false;
            resetTiredTimer();
            resetSpeedMultiplier();
        }
    }

    void resetTiredTimer()
    {
        tiredTimer = 0;
    }

    void resetSpeedMultiplier()
    {
        speedMultiplier = 1;
    }

    public void Death()
    {
        velocity = 0;

        anim.gameObject.GetComponent<Animator>().enabled = false;

        left[0].SetVelocity(0);
        left[1].SetVelocity(0);
        left[2].SetVelocity(0);
        left[3].SetVelocity(0);
    }
}
