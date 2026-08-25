using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private bool sent;

    [SerializeField] private Text currentTimeText;
    [SerializeField] private Text gameOverTimeText;

    private void Awake()
    {
        GameManager.singleton.duration = 0;
    }

    void Update()
    {
        if (GameManager.singleton.isGameOver == false)
        {
            if (sent) sent = false;
            GameManager.singleton.duration = GameManager.singleton.duration + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(GameManager.singleton.duration);
        currentTimeText.text = time.ToString(@"mm\:ss\:ff");

        gameOverTimeText.text = "Time: " + time.ToString(@"mm\:ss\:ff");
    }
}
