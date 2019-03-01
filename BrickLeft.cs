using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickLeft : MonoBehaviour
{
    public Rigidbody2D rigidBod;
    private Character character;
    private float velocity;
    float xRange;
    // Start is called before the first frame update
    void Start()
    {
        rigidBod = GetComponent<Rigidbody2D>();
        character = FindObjectOfType<Character>();
        velocity = -2f;
        xRange = Random.Range(-3.7f, -0.3f);
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
            xRange = Random.Range(-3.7f, 0.1f);
            Debug.Log("xRange: " + xRange);
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
