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
    [SerializeField] private GameObject loseTrigger;

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
        uiManager.SetScoreText(score);
        uiManager.SetBallsCountToScreen(gunManager.ball_count);
        if (!gameInPause) { Shooting(); }
    }

    void Shooting()
    {
        //if (Input.GetTouch(0).phase == TouchPhase.Began && !onAction) { gunManager.FreezRotation(false); }
        if (Input.GetMouseButtonDown(0) && !onAction) { gunManager.FreezRotation(false); }

        //if (Input.GetTouch(0).phase == TouchPhase.Ended && !onAction)
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
            if (figManager.GetMaxtPosition() >= loseTrigger.transform.position.y)
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
        gunManager.FreezRotation(true);
        gameInPause = true;
        figManager.DestroyAllFigures();
    }
}
