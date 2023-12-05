using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{

    public int score = 0;
    public int bestScore = 0;
    
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BEST_SCORE", 0);
    }

    void Update()
    {
    }
    
    public void IncreaseScore(int amount)
    {
        score += amount;
    }
    
    public void SetBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BEST_SCORE", bestScore);
        }
    }
}