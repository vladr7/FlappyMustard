using System.Collections;
using System.Collections.Generic;
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
