using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTriggerScript : MonoBehaviour
{
    public LogicManager logicManager;
    public GameObject gameOverBackground;
    private float _lastEntryTime;
    public float endGameTriggerDelay = 2.5f;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        _lastEntryTime = Time.time;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time - _lastEntryTime > endGameTriggerDelay && !logicManager.gameHasEnded)
        {
            gameOverBackground.SetActive(true);
            logicManager.GameOver();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _lastEntryTime = 0.0f;
    }

}
