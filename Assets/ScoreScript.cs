using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{

    public LogicManager logicManager;
    public TextMeshProUGUI scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = logicManager.score.Value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = logicManager.score.Value.ToString();
    }
}
