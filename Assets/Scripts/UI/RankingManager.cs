using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using static APIHandler;
using System;

public class RankingManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI[] nameText;
    public TMPro.TextMeshProUGUI[] scoreText;
    public Score[] scores;

    public TMPro.TextMeshProUGUI bestName;
    public TMPro.TextMeshProUGUI bestPosition;
    public TMPro.TextMeshProUGUI bestValue;

    void Start()
    {
        UpdateInfo();
    }

    void OnEnable()
    {
        UpdateInfo();
    }

    async public void UpdateInfo()
    {
        try
        {
            scores = await GetScores(0, 10);
        }
        catch (Exception error)
        {
            scores = Array.Empty<Score>();
            Debug.LogWarning("Online ranking is unavailable. " + error.Message);
        }

        UpdateTexts();
    }

    void UpdateTexts()
    {
        int visibleScores = Math.Min(scores.Length, Math.Min(nameText.Length, scoreText.Length));
        for (int i = 0; i < visibleScores; i++)
        {
            Score score = scores[i];
            nameText[i].text = score.position.ToString() + ". " + score.player;
            scoreText[i].text = score.value.ToString();
        }

        for (int i = visibleScores; i < Math.Min(nameText.Length, scoreText.Length); i++)
        {
            nameText[i].text = string.Empty;
            scoreText[i].text = string.Empty;
        }


        if (GameManager.singleton.player == null)
        {
            return;
        }

        bestName.text = GameManager.singleton.player.name + "'s best:";
        Score bestScore = GameManager.singleton.bestScore;

        if (bestScore != null)
        {
            bestPosition.text = "#" + bestScore.position;
            bestValue.text = bestScore.value.ToString();
        }
    }
}
