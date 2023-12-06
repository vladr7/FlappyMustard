using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateFinalScore(int score, bool newBestScore)
    {
        if(newBestScore)
            gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "New best score!\n" + score;
        else
            gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Your score\n" + score;
    }
}
