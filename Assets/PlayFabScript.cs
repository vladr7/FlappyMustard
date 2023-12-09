using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayFabSettings.TitleId = "C56EF";
        SignIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdatePlayerScore(int bestScore)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new PlayFab.ClientModels.UpdatePlayerStatisticsRequest
            {
                Statistics = new List<PlayFab.ClientModels.StatisticUpdate>
                {
                    new()
                    {
                        StatisticName = "BestScore",
                        Value = bestScore
                    }
                }
            }, result => { Debug.Log("User statistics updated"); },
            error => { Debug.Log(error.GenerateErrorReport()); });
    }

    public async Task<List<PlayerNameScore>> GetLeaderboardAsync()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "BestScore",
            StartPosition = 0,
            MaxResultsCount = 100
        };

        try
        {
            var result = await GetLeaderboardTask(request);
            var leaderboard = new List<PlayerNameScore>();
            foreach (var player in result.Leaderboard)
            {
                leaderboard.Add(new PlayerNameScore(player.DisplayName, player.StatValue));
            }
            return leaderboard;
        }
        catch (System.Exception error)
        {
            Debug.LogError($"Error retrieving leaderboard: {error.ToString()}");
            return null; // Or handle the error as appropriate
        }
    }

    private Task<GetLeaderboardResult> GetLeaderboardTask(GetLeaderboardRequest request)
    {
        var tcs = new TaskCompletionSource<GetLeaderboardResult>();

        PlayFabClientAPI.GetLeaderboard(request, result =>
            {
                tcs.SetResult(result);
            },
            error =>
            {
                tcs.SetException(new Exception(error.GenerateErrorReport()));
            });

        return tcs.Task;
    }
    
    public void SignIn()
    {
        string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
        var request = new LoginWithCustomIDRequest { CustomId = deviceUniqueIdentifier, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
    
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login successful!");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError($"Login failed: {error.GenerateErrorReport()}");
    }
}
