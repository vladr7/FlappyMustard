using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 10;
    public float deadZone = -45;
    public ParticleSystem clouds;
    public float cloudSpeedBoost = 3;
    public BirdScript birdScript;
    
    // Start is called before the first frame update
    void Start()
    {
        clouds = GameObject.FindGameObjectWithTag("Clouds").GetComponent<ParticleSystem>();
        birdScript = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * (moveSpeed * Time.deltaTime);

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
        turboBoost();
    }

    public void turboBoost()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            birdScript.setTurbo(true);
            var main = clouds.main;
            main.simulationSpeed = cloudSpeedBoost;
            Time.timeScale = 2.0f;
            Invoke(nameof(resetSpeed), 1);
        }
    }
    
    private void resetSpeed()
    {
        birdScript.setTurbo(false);
        Time.timeScale = 1.0f;
        var main = clouds.main;
        main.simulationSpeed = 1;
    }
    
}
