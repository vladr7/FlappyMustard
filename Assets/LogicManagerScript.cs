using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
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
    public PlayFabScript playFabScript;

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BEST_SCORE", 0);
        ManageServerConnection();
        Debug.Log("Best score: " + bestScore);
    }

    private async void ManageServerConnection()
    {
        bool successSignIn = await playFabScript.SignInAsync();
        if (successSignIn)
        {
            Debug.Log("Sign in successful");
            SetPlayerName();
            DisplayLeaderboard();
        }
        else
        {
            Debug.Log("Sign in failed");
        }
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
    
    private async void DisplayLeaderboard()
    {
        var result = await playFabScript.GetLeaderboardAsync();
        foreach (var playerNameScore in result)
        {
            Debug.Log("Leaderboard: " + playerNameScore.Name + ": " + playerNameScore.Score);
        }
        // leaderboardScript.UpdateLeaderboard(result);
    }
    
    private async void SetPlayerName()
    {
        string userName = PlayerPrefs.GetString("USER_NAME", "");
        if(userName == "")
            return;
        var result = await playFabScript.SetPlayerDisplayNameAsync(userName);
        Debug.Log("Player name set: " + result.ToString());
    }

    public void SetBestScore()
    {
        if (score > bestScore)
        {
            Debug.Log("New best score: " + score);
            bestScore = score;
            playFabScript.UpdatePlayerScore(bestScore);
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