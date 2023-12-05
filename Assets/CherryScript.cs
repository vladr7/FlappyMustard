using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryScript : MonoBehaviour
{
    public StrawberryScript strawberryScript;
    public LogicManager logicManager;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Cherry"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            if (this.gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                strawberryScript.SpawnStrawberry(transform.position.x, transform.position.y);
                logicManager.IncreaseScore(1);
            }
        }

    }
    
    
}
