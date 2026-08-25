using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text textScore;
    [SerializeField] private Text textBest;

    [SerializeField] private TextMeshProUGUI textGameOverScore;
    [SerializeField] private TextMeshProUGUI textGameOverBest;

    void Update()
    {
        Score bestScore = GameManager.singleton.bestScore;
        if (bestScore != null)
        {
            textBest.text = "Best: " + bestScore.value;
            textGameOverBest.text = "Best #" + bestScore.position + " - " + bestScore.value;
        }

        textScore.text = "" + GameManager.singleton.score;
        textGameOverScore.text = "Score  " + GameManager.singleton.score;
    }
}
