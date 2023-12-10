using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardScript : MonoBehaviour
{
    public TextMeshProUGUI firstPlaceText;
    public TextMeshProUGUI secondPlaceText;
    public TextMeshProUGUI thirdPlaceText;
    public TextMeshProUGUI fourthPlaceText;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void UpdateLeaderboard(List<PlayerNameScore> leaderboard)
    {
        if(leaderboard.Count < 1 || leaderboard[0] == null)
            return;
        firstPlaceText.text = leaderboard[0].Name + leaderboard[0].Score;
        if(leaderboard.Count < 2 || leaderboard[1] == null)
            return;
        secondPlaceText.text = leaderboard[1].Name + leaderboard[1].Score;
        if(leaderboard.Count < 3 || leaderboard[2] == null)
            return;
        thirdPlaceText.text = leaderboard[2].Name + leaderboard[2].Score;
        if(leaderboard.Count < 4 || leaderboard[3] == null)
            return;
        fourthPlaceText.text = leaderboard[3].Name + leaderboard[3].Score;
    }
}
