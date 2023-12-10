using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardScrollScript : MonoBehaviour
{
    public GameObject leaderboardEntryPrefab;
    public GameObject leaderboardContent;

    public void PopulateLeaderboard(List<PlayerNameScore> leaderboard, string currentUserName)
    {
        foreach (Transform child in leaderboardContent.transform) 
        {
            Destroy(child.gameObject);
        }

        foreach (var entry in leaderboard)
        {
            GameObject newEntry = Instantiate(leaderboardEntryPrefab, leaderboardContent.transform); 

            TMP_Text entryText = newEntry.GetComponent<TMP_Text>();
            entryText.text = $"{entry.Name}: {entry.Score}";

            if (entry.Name == currentUserName)
            {
                entryText.color = Color.green;
            }
        }
    }
}