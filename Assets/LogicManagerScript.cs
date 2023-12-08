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
    public AudioSource gameOverSFX;
    public GameObject mainTheme;
    public bool gameHasEnded = false;
    
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BEST_SCORE", 0);
        Debug.Log("Best score: " + bestScore);
    }

    void Update()
    {
    }
    
    public void IncreaseScore(int amount)
    {
        if (!gameHasEnded)
        {
            score += amount;
        }
    }
    
    public void SetBestScore()
    {
        if (score > bestScore)
        {
            Debug.Log("New best score: " + score);
            bestScore = score;
            finalScoreScript.UpdateFinalScore(score, true);
            PlayerPrefs.SetInt("BEST_SCORE", bestScore);
        }
        else
        {
            Debug.Log("Final score: " + score);
            finalScoreScript.UpdateFinalScore(score, false);
        }
    }
    
    public void RestartGame()
    {
        gameHasEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameHasEnded = true;
        SetBestScore();
        gameOverSFX.Play();
        AudioSource mainThemeAudioSource = mainTheme.GetComponent<AudioSource>();
        mainThemeAudioSource.Stop();
        gameOverScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}