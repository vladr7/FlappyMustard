using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScoreScript : MonoBehaviour
{
    public LogicManager logicManager;
    public TextMeshProUGUI bestScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        bestScoreText.text = "Best Score\n" + logicManager.bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
