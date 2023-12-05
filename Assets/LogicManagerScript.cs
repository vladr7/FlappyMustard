using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{

    public int score = 0;
    
    void Start()
    {
        
    }

    void Update()
    {
    }
    
    public void IncreaseScore(int amount)
    {
        score += amount;
    }
}