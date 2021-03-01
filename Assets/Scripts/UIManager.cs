﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject losePanel;



    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetUIPanelActive(bool check)
    {
        scorePanel.SetActive(!check);
        losePanel.SetActive(check);
        bestScoreText.text = scoreText.text;
    }
}
