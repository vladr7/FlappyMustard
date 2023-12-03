using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 10;
    public float deadZone = -45;
    public float turboStrength = 10;
    private float _defaultMoveSpeed;
    public ParticleSystem clouds;
    public float cloudSpeedBoost = 3;

    // Start is called before the first frame update
    void Start()
    {
        clouds = GameObject.FindGameObjectWithTag("Clouds").GetComponent<ParticleSystem>();
        _defaultMoveSpeed = moveSpeed;
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
            moveSpeed += turboStrength;
            var main = clouds.main;
            main.simulationSpeed = cloudSpeedBoost;
            Invoke(nameof(resetSpeed), 1);
        }
    }
    
    private void resetSpeed()
    {
        moveSpeed = _defaultMoveSpeed;
        var main = clouds.main;
        main.simulationSpeed = 1;
    }
    
}
