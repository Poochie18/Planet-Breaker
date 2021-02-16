using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnClick;
    public static event Action OnAfterAction;

    [SerializeField] private int level = 1;
    [SerializeField] private FiguresManager figManager;
    [SerializeField] private GunManager gunManager;

    private bool onAction = false;

    void Awake()
    {
        OnClick += ChangeSwitches;

        OnAfterAction += ChangeSwitches;
    }

    void Start()
    {
        figManager.PlacingFigures(gunManager);
    }

    void Update()
    {
        Shooting();
    }

    void Shooting()
    {

        if (Input.GetMouseButtonDown(0) && !onAction)
        {
            level += 1;
            OnClick();
        }
        else if (gunManager.CheckBallsCount() && onAction)
        {
            OnAfterAction();
            figManager.PlacingFigures(gunManager);
        }
    }


    void ChangeSwitches()
    {
        onAction = !onAction;
        gunManager.FreezRotation();
    }
}
