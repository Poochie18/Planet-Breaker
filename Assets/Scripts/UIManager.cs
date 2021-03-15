using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text ballsCountText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Text bestBallsCount;

    [SerializeField] private Button restartButton;

    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject losePanel;


    public void SetBallsCountToScreen(int balls)
    {
        ballsCountText.text = balls.ToString();
    }

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetUIPanelActive(bool check)
    {
        scorePanel.SetActive(!check);
        losePanel.SetActive(check);
        bestScoreText.text = scoreText.text;
        bestBallsCount.text = ballsCountText.text;
    }
}
