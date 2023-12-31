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
    
    public async Task<bool> SignInAsync()
    {
        string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
        var request = new LoginWithCustomIDRequest { CustomId = deviceUniqueIdentifier, CreateAccount = true };

        try
        {
            await LoginWithCustomIDTask(request);
            Debug.Log("Login successful!");
            return true;
        }
        catch (PlayFabException error)
        {
            Debug.LogError($"Login failed: {error}");
            return false;
        }
    }

    private Task LoginWithCustomIDTask(LoginWithCustomIDRequest request)
    {
        var tcs = new TaskCompletionSource<bool>();

        PlayFabClientAPI.LoginWithCustomID(request, result =>
            {
                tcs.SetResult(true);
            },
            error =>
            {
                tcs.SetException(new PlayFabException(PlayFabExceptionCode.BuildError, "Failed to login"));
            });

        return tcs.Task;
    }
    
    public async Task<bool> SetPlayerDisplayNameAsync(string displayName)
    {
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = displayName };
        try
        {
            var result = await UpdateDisplayNameTask(request);
            Debug.Log("Successfully updated display name to: " + result.DisplayName);
            return true; // Return true for success
        }
        catch (PlayFabException error)
        {
            Debug.LogError("Failed to update display name: " + error.ToString());
            return false; // Return false in case of failure
        }
    }

    private Task<UpdateUserTitleDisplayNameResult> UpdateDisplayNameTask(UpdateUserTitleDisplayNameRequest request)
    {
        var tcs = new TaskCompletionSource<UpdateUserTitleDisplayNameResult>();

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, result =>
            {
                tcs.SetResult(result);
            },
            error =>
            {
                tcs.SetException(new PlayFabException(PlayFabExceptionCode.BuildError, "Failed to update display name"));
            });

        return tcs.Task;
    }
}
