using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public Rigidbody2D rigidBod;
    public Animator anim;
    public float runSpeed;                                                  //runSpeed is the speed set by user
    float velocity;                                                         //velocity is used to change direction of player using runSpeed
    float speedMultiplier = 1;
    float widthWorldUnits;

    public BrickLeft[] left;
    public GameMenu gameMenu;

    // Start is called before the first frame update
    void Start()
    {
        rigidBod = GetComponent<Rigidbody2D>();
        velocity = -runSpeed;
        left = FindObjectsOfType<BrickLeft>();
        anim = GetComponent<Animator>();
        gameMenu = FindObjectOfType<GameMenu>();

        widthWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - 0.35f; //Camera aspect=ratio. OrtographicSize=5. 0.35f=fixed around half size of character
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
        if (transform.position.x < -widthWorldUnits)
        {
            Vector3 charscale = transform.localScale;
            charscale.x *= -1;                                              //multiplies value of Vector3.x by -1 to change direction
            transform.localScale = charscale;
            velocity = runSpeed;
        }
        else if (transform.position.x > widthWorldUnits)
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

        gameMenu.ToggleEndMenu();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
