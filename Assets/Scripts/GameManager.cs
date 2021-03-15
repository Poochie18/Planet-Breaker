using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    //Ball Shot Event
    public static event Action OnClick;
    //Event when all balls are collected
    public static event Action OnAfterAction;
    //Event at the start of the game
    public static event Action OnStartGame;
    //Loss event
    public static event Action OnLoseGame;
    //Event for creating new shapes
    public static event Action PlaceNewFigures;

    [SerializeField] private static int level;
    [SerializeField] private GameObject loseTrigger;

    [SerializeField] private bool gameInPause = false;

    [SerializeField] private FiguresManager figManager;
    [SerializeField] private GunManager gunManager;
    [SerializeField] private UIManager uiManager;

    private static int score = 0;
    //Is there an action on the field or not
    private bool gameOnAction = false;

    private void Awake()
    {
        OnClick += ChangeSwitches;

        OnAfterAction += ChangeSwitches;
    }

    void Start()
    {
        //Resetting Counters and Positioning Initial Figures
        level = 1;
        gunManager.ball_count = 1;
        score = 0;
        for (int j = 0; j <= 1; j++)
        {
            PlaceNewFigures();
            OnStartGame();
        }
        PlaceNewFigures();

        //Activating the counter panel and deactivating the defeat panel
        uiManager.SetUIPanelActive(false);
    }

    void Update()
    {
        //Counter update
        uiManager.SetScoreText(score);
        //Updating the number of balls
        uiManager.SetBallsCountToScreen(gunManager.ball_count);
        //If the game is not paused, then you can press
        if (!gameInPause) { Shooting(); }
    }

    void Shooting()
    {
        //Rotation of the cannon after pressing and holding on the screen
        //if (Input.GetTouch(0).phase == TouchPhase.Began && !onAction) { gunManager.FreezRotation(false); }
        if (Input.GetMouseButtonDown(0) && !gameOnAction) { gunManager.FreezRotation(false); }

        //Shooting the ball after releasing the key
        //if (Input.GetTouch(0).phase == TouchPhase.Ended && !onAction)
        if (Input.GetMouseButtonUp(0) && !gameOnAction)
        {
            level += 1;

            //On event, the method in the gun manager script is triggered
            OnClick();

            //Freeze the rotation of the cannon while the balls are on the field
            gunManager.FreezRotation(true);
        }
        //Checking that all balls hit the collector
        else if (gunManager.CheckBallsCount() && gameOnAction)
        {
            //A trigger is triggered to move all pieces and bonuses up after all balls hit the collector
            OnAfterAction();

            //Create a new shape layer
            PlaceNewFigures();

            //If the topmost piece on the square is higher than the defeat trigger, 
            //then the GameIsOver() method starts
            if (figManager.GetMaxtPosition() >= loseTrigger.transform.position.y)
                GameIsOver();
        }

    }

    //Returns the current level
    public static int GetCurrentLevel() { return level; }

    //Returns the current score
    public static int GetCurrentScore() { return score; }

    //Adds one to the current score
    public static void AddScore() { score += 1; }

    //Changes the value of a boolean variable depending on the state of the game
    private void ChangeSwitches(){ gameOnAction = !gameOnAction; }
    
    private void GameIsOver()
    {
        //Sets the defeat bar and removes the game bar
        uiManager.SetUIPanelActive(true);

        //When defeated, stops the rotation of the cannon
        gunManager.FreezRotation(true);

        //Pauses the game
        gameInPause = true;

        //Connects the defeat event
        OnLoseGame();
    }
}
