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
    public AudioSource flapSFX;
    public ParticleSystem turbo;
    public ParticleSystem explosion;
    public bool turboActive = false;
    public bool hasTurbo = true;
    public GameObject turboBar;
    private GameObject firstTurbo;
    private GameObject secondTurbo;
    private GameObject thirdTurbo;


    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        firstTurbo = turboBar.transform.GetChild(0).gameObject;
        secondTurbo = turboBar.transform.GetChild(1).gameObject;
        thirdTurbo = turboBar.transform.GetChild(2).gameObject;
        Debug.Log("Logging!");
    }

    // Update is called once per frame
    void Update()
    {
        ManageTurboBar();
        if (turboActive)
        {
            if (!turbo.isPlaying)
            {
                turbo.Play();
            }

            if (!explosion.isPlaying)
            {
                explosion.Play();
            }
        }
        else
        {
            if (turbo.isPlaying)
            {
                turbo.Stop();
            }

            if (explosion.isPlaying)
            {
                explosion.Stop();
            }
        }

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

        flap();
    }

    private void ManageTurboBar()
    {
        hasTurbo = logic.numberOfTurbo > 0;
        if (logic.numberOfTurbo <= 0)
        {
            firstTurbo.SetActive(false);
            secondTurbo.SetActive(false);
            thirdTurbo.SetActive(false);
        }
        else if (logic.numberOfTurbo == 1)
        {
            firstTurbo.SetActive(true);
            secondTurbo.SetActive(false);
            thirdTurbo.SetActive(false);
        }
        else if (logic.numberOfTurbo == 2)
        {
            firstTurbo.SetActive(true);
            secondTurbo.SetActive(true);
            thirdTurbo.SetActive(false);
        }
        else if (logic.numberOfTurbo >= 3)
        {
            firstTurbo.SetActive(true);
            secondTurbo.SetActive(true);
            thirdTurbo.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        birdIsAlive = false;
        logic.gameOver();
    }

    private void flap()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flapSFX.Play();
            myRigidbody.velocity = Vector2.up * flapStrength;
        }
    }

    public void setTurbo(bool active)
    {
        turboActive = active;
    }
}