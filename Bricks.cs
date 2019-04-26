using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    public Rigidbody2D rigidBod;
    private float velocity;
    float horizontalCorner;
    float xRange;
    public float originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        rigidBod = GetComponent<Rigidbody2D>();
        horizontalCorner = Camera.main.aspect * Camera.main.orthographicSize - 1f; //0.8f is half the gap
        horizontalCorner = horizontalCorner * 0.6f;
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
        velocity = -3.4f;
        xRange = Random.Range(-horizontalCorner, horizontalCorner); //0.65f to not touch the corner
        rigidBod.position = new Vector2(xRange, rigidBod.position.y);
    }

    void OnGameOverConfirmed()
    {
        rigidBod.position = new Vector2(xRange, originalPosition);
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
        if (transform.position.y < -15)
        {
            xRange = Random.Range(-horizontalCorner, horizontalCorner);
            rigidBod.position = new Vector2(xRange, 5);
        }
    }

    public void SetVelocity(float vel)
    {
        velocity = vel;
    }
}
