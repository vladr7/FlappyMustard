using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{

    public int score = 0;
    public int bestScore = 0;
    public GameObject gameOverScreen;
    public FinalScoreScript finalScoreScript;
    
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
            finalScoreScript.UpdateFinalScore(score, true);
            PlayerPrefs.SetInt("BEST_SCORE", bestScore);
        }
        else
        {
            finalScoreScript.UpdateFinalScore(score, false);
        }
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        SetBestScore();
        // deadBirdSFX.Play();
        Time.timeScale = 0.0f;
        gameOverScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}