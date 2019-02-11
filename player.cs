using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Rigidbody2D rigidBod;
    public float runSpeed;
    float accelerationClick;
    public float tiredSpeed;
    public bool lookright = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBod = GetComponent<Rigidbody2D>();
        accelerationClick = runSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(transform.position.x);
        Debug.Log(rigidBod.velocity);


        if (transform.position.x < -2)
        {
            accelerationClick = runSpeed;
            Flip();
        }
        else if (transform.position.x > 2)
        {
            accelerationClick = -runSpeed;
            Flip();
        }

        rigidBod.velocity = new Vector2(accelerationClick, rigidBod.velocity.y);
    }

    void Flip()
    {
        lookright = !lookright;
        Vector3 charscale = transform.localScale;
        charscale.x *= -1;
        transform.localScale = charscale;
    }
}
