using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    public Rigidbody2D rigidBod;
    public Animator anim;
    public float runSpeed;                                                  //runSpeed is the speed set by user
    float velocity;                                                         //velocity is used to change direction of player using runSpeed
    float speedMultiplier = 1;
    float widthWorldUnits;
    bool side = false;                                                       //side: false=left, true=right

    public Bricks[] bricks;

    // Start is called before the first frame update
    void Start()
    {
        rigidBod = GetComponent<Rigidbody2D>();
        bricks = FindObjectsOfType<Bricks>();
        anim = GetComponent<Animator>();
        anim.gameObject.GetComponent<Animator>().enabled = false;

        widthWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - 0.35f; //Camera aspect=ratio. OrtographicSize=5. 0.35f=fixed around half size of character
        Debug.Log("widthWorldUnits" + widthWorldUnits);
    }

    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted()
    {
        velocity = runSpeed;
        rigidBod.simulated = true;
        side = false;
        anim.gameObject.GetComponent<Animator>().enabled = true;
        widthWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - 0.35f; //Camera aspect=ratio. OrtographicSize=5. 0.35f=fixed around half size of character
    }

    void OnGameOverConfirmed()
    {
        transform.localPosition = new Vector2(0, -3.9f);
        Vector3 charscale = transform.localScale;
        charscale.x = 1;
        transform.localScale = charscale;
    }

    // Update is called once per frame
    void Update()
    {
        //handles input; if click=true, start clickCounter; accelerate for 2s from last click
        if (Input.GetKey("right"))
        {
            speedMultiplier = 3f;
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
        if (rigidBod.position.x < -widthWorldUnits && side)
        {
            Vector3 charscale = transform.localScale;
            charscale.x *= -1;                                              //multiplies value of Vector3.x by -1 to change direction
            transform.localScale = charscale;
            velocity = runSpeed;
            side = false;
        }
        else if (rigidBod.position.x > widthWorldUnits && !side)
        {
            Vector3 charscale = transform.localScale;
            charscale.x *= -1;
            transform.localScale = charscale;
            velocity = -runSpeed;
            side = true;
        }
    }


    void resetSpeedMultiplier()
    {
        speedMultiplier = 1;
    }

    public void Death()
    {
        rigidBod.simulated = false; //stops listening to physics

        anim.gameObject.GetComponent<Animator>().enabled = false;

        bricks[0].SetVelocity(0);
        bricks[1].SetVelocity(0);
        bricks[2].SetVelocity(0);
        bricks[3].SetVelocity(0);

        //gameMenu.ToggleEndMenu();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ScoreZone")
        {
            OnPlayerScored();
        }

        if (collision.gameObject.tag == "DeadZone")
        {
            Debug.Log("You ded");
            Death();
            OnPlayerDied();
        }
    }
}
