using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength = 10;
    public LogicScript logic;
    public bool birdIsAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        Debug.Log("Logging!");
    }

    // Update is called once per frame
    void Update()
    {
        if (!birdIsAlive)
        {
            return;
        }

        if (myRigidbody.transform.position.y > 20 || myRigidbody.transform.position.y < -20)
        {
            birdIsAlive = false;
            logic.gameOver();
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        birdIsAlive = false;
        logic.gameOver();
    }
}
