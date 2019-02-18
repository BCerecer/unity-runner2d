using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Rigidbody2D rigidBod;
    public float runSpeed;                                                  //runSpeed is the speed set by user
    float velocity;                                                         //velocity is used to change direction of player using runSpeed
    float accelerationClick;
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
        //Debug.Log(transform.position.x);
        //Debug.Log("Velocity " + rigidBod.velocity);
                   

    }

    void FixedUpdate()
    {
        rigidBod.velocity = new Vector2(velocity + accelerationClick, rigidBod.velocity.y); 
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
}
