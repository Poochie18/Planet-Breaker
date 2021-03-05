using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static event Action OnClick;
    public static event Action OnAfterAction;
    public static event Action OnStartGame;

    [SerializeField] private static int level;
    [SerializeField] private float loseDistance;

    [SerializeField] private bool gameInPause = false;
    [SerializeField] private FiguresManager figManager;
    [SerializeField] private GunManager gunManager;
    [SerializeField] private UIManager uiManager;

    public static int score = 0;
    private bool onAction = false;

    void Awake()
    {
        OnClick += ChangeSwitches;

        OnAfterAction += ChangeSwitches;
    }

    void Start()
    {
        level = 1;
        gunManager.ball_count = 1;
        for (int j = 0; j <= 1; j++)
        {
            figManager.PlacingFigures();
            OnStartGame();
        }

        score = 0;
        uiManager.SetUIPanelActive(false);
        figManager.PlacingFigures();
    }

    void Update()
    {
        if (!gameInPause) { Shooting(); }
        uiManager.SetScoreText(score);
        uiManager.SetBallsCountToScreen(gunManager.ball_count);
    }

    void Shooting()
    {
        //if (Input.GetTouch(0).phase == TouchPhase.Began && !onAction) { gunManager.FreezRotation(false); }
        if (Input.GetMouseButtonDown(0) && !onAction) { gunManager.FreezRotation(false); }
        //if(Input.GetTouch(0).phase == TouchPhase.Ended && !onAction)
        if (Input.GetMouseButtonUp(0) && !onAction)
        {
            level += 1;
            OnClick();
            gunManager.FreezRotation(true);
        }
        else if (gunManager.CheckBallsCount() && onAction)
        {
            OnAfterAction();
            figManager.PlacingFigures();
            if (figManager.GetMaxtPosition() >= loseDistance)
                GameIsOver();
        }
        
    }

    public static int GetCurrentLevel()
    {
        return level;
    }

    void ChangeSwitches()
    {
        onAction = !onAction;
    }

    void GameIsOver()
    {
        uiManager.SetUIPanelActive(true);
        gunManager.FreezRotation(false);
        gameInPause = true;
        figManager.DestroyAllFigures();
    }
}
