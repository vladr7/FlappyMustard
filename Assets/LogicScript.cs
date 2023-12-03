using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public BirdScript birdScript;
    public AudioSource dingSFX;
    public AudioSource deadBirdSFX;
    public Text highScoreText;

    public void Start()
    {
        birdScript = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>();
        dingSFX.volume = 0.2f;
        int highScore = PlayerPrefs.GetInt("HIGH_SCORE");
        highScoreText.text = highScore.ToString();
    }

    [ContextMenu("Increase Score")]
    public void addScore()
    {
        if (birdScript.birdIsAlive)
        {
            dingSFX.Play();
            playerScore += 1;
            scoreText.text = playerScore.ToString();
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        int oldHighScore = PlayerPrefs.GetInt("HIGH_SCORE");
        if (playerScore > oldHighScore)
        {
            PlayerPrefs.SetInt("HIGH_SCORE", playerScore);
        }
        deadBirdSFX.Play();
        gameOverScreen.SetActive(true);
    }
    
    public void quitGame()
    {
        Application.Quit();
    }
}