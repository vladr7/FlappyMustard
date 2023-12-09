public class PlayerNameScore
{
    public string Name { get; set; }
    public int Score { get; set; }

    public PlayerNameScore(string name, int score)
    {
        Name = name;
        Score = score;
    }
}