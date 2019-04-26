using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCollider : MonoBehaviour
{
    private Character character;

    // Start is called before the first frame update
    void Start()
    {
        character = FindObjectOfType<Character>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("You ded");
            character.Death();
        }
    }
}
