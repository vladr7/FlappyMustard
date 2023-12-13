using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : NetworkBehaviour
{
    public int bestScore = 0;
    public GameObject gameOverScreen;
    public FinalScoreScript finalScoreScript;
    public AudioSource gameOverSFX;
    public GameObject mainTheme;
    public bool gameHasEnded = false;
    public PlayFabScript playFabScript;
    public LeaderboardScript leaderboardScript;
    public string userName = "";
    public LeaderboardScrollScript leaderboardScrollScript;
    public GameObject pauseScreen;
    public GameObject papyrus;
    public AudioSource mainThemeAudioSource;
    public bool isPaused = false;

    public NetworkVariable<int> score = new NetworkVariable<int>(
        0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );

    public override void OnNetworkSpawn()
    {
        score.OnValueChanged += (int previousValue, int newValue) =>
        {
            Debug.Log(OwnerClientId + " ; score: " + score.Value);
        };
    }

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
        HandlePauseMenu();
    }

    private void HandlePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameHasEnded)
                return;
            if (Time.timeScale == 1)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        papyrus.SetActive(true);
        mainThemeAudioSource.Pause();
        pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        papyrus.SetActive(false);
        mainThemeAudioSource.Play();
        pauseScreen.SetActive(false);
    }

    [ServerRpc]
    public void IncreaseScoreServerRpc(int amount)
    {
        if (!IsOwner)
        {
            Debug.LogError("Only the owner can increase the score.");
            return;
        }

        if (gameHasEnded)
        {
            Debug.LogError("Game has ended.");
            return;
        }

        score.Value += amount;
        Debug.Log("Current score: " + score.Value);
    }

    private async void DisplayLeaderboard()
    {
        var result = await playFabScript.GetLeaderboardAsync();
        foreach (var playerNameScore in result)
        {
            Debug.Log("Leaderboard: " + playerNameScore.Name + ": " + playerNameScore.Score);
        }

        leaderboardScript.UpdateLeaderboard(result, userName);
        leaderboardScrollScript.PopulateLeaderboard(result, userName);
    }

    private async void SetPlayerName()
    {
        userName = PlayerPrefs.GetString("USER_NAME", "");
        if (userName == "")
            return;
        var result = await playFabScript.SetPlayerDisplayNameAsync(userName);
        Debug.Log("Player name set: " + result.ToString());
    }

    public void SetBestScore()
    {
        if (score.Value > bestScore)
        {
            Debug.Log("New best score: " + score);
            bestScore = score.Value;
            playFabScript.UpdatePlayerScore(bestScore);
            finalScoreScript.UpdateFinalScore(score.Value, true);
            PlayerPrefs.SetInt("BEST_SCORE", bestScore);
        }
        else
        {
            Debug.Log("Final score: " + score);
            finalScoreScript.UpdateFinalScore(score.Value, false);
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
        leaderboardScrollScript.gameObject.SetActive(true);
        Invoke(nameof(DisplayLeaderboard), 0.3f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}