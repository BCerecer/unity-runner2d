using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickLeft : MonoBehaviour
{
    public Rigidbody2D rigidBod;
    private Character character;
    private float velocity;
    float horizontalCorner;
    float xRange;
    // Start is called before the first frame update
    void Start()
    {
        rigidBod = GetComponent<Rigidbody2D>();
        character = FindObjectOfType<Character>();
        velocity = -2f;
        horizontalCorner = Camera.main.aspect * Camera.main.orthographicSize - 1f; //0.8f is half the gap
        xRange = Random.Range(-horizontalCorner, horizontalCorner);
        rigidBod.position = new Vector2(xRange, rigidBod.position.y);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        rigidBod.velocity = new Vector2(0, velocity);
        OutOfBounds();
    }

    void OutOfBounds()
    {
        if (transform.position.y < -18)
        {
            xRange = Random.Range(-horizontalCorner, horizontalCorner);
            rigidBod.position = new Vector2(xRange, 6);
        }
    }

    public void SetVelocity(float vel)
    {
        velocity = vel;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            character.Death();
        }
    }
}
