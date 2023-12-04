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
    public int numberOfTurbo = 3;
    public ParticleSystem clouds;
    public float cloudSpeedBoost = 3;
    public PipeSpawnScript pipeSpawnScript;

    public void Start()
    {
        birdScript = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>();
        clouds = GameObject.FindGameObjectWithTag("Clouds").GetComponent<ParticleSystem>();
        dingSFX.volume = 0.2f;
        int highScore = PlayerPrefs.GetInt("HIGH_SCORE");
        highScoreText.text = highScore.ToString();
    }

    public void Update()
    {
        turboBoost();
    }

    [ContextMenu("Increase Score")]
    public void addScore()
    {
        if (birdScript.birdIsAlive)
        {
            dingSFX.Play();
            playerScore += 1;
            if (numberOfTurbo < 3)
            {
                numberOfTurbo++;
            }

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

    public void turboBoost()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && birdScript.hasTurbo)
        {
            if (numberOfTurbo > 0)
            {
                numberOfTurbo--;
            }
            birdScript.setTurbo(true);
            var main = clouds.main;
            main.simulationSpeed = cloudSpeedBoost;
            Time.timeScale = 2.0f;
            Invoke(nameof(resetSpeed), 1);
        }
    }
    
    private void resetSpeed()
    {
        birdScript.setTurbo(false);
        Time.timeScale = 1.0f;
        var main = clouds.main;
        main.simulationSpeed = 1;
    }
}