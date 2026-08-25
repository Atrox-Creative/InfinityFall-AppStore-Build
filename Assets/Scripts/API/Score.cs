using System.Collections.Generic;

[System.Serializable]
public class Score
{
    public string _id;
    public string player;
    public int value;
    public int duration;
    public int maxCombo = 1;
    public int position;

    public Score(string playerId, int scoreValue, int scoreDuration, int scorePosition)
    {
        player = playerId;
        value = scoreValue;
        duration = scoreDuration;
        position = scorePosition;
    }
}