[System.Serializable]
public class Score
{
    public string name;
    public int highscore;
}

[System.Serializable]
public class Scores
{
    public Score[] scores;
}