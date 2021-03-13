using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bonus : MonoBehaviour
{
    [SerializeField] private float bonusSize = 0.06f;
    [SerializeField] private int bonusHP;

    [SerializeField] private SpriteRenderer sp2D;

    public bool IsAlive => bonusHP > 0;
    private GunManager gunManager;

    void Start()
    {
        transform.localScale = new Vector2(1f, 1f) * bonusSize;
        SetColor();
    }

    public void SetGunManager(GunManager gun)
    {
        gunManager = gun;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        gunManager.InstantiateBall(col.gameObject.transform.position);
        bonusHP = 0;
        gameObject.SetActive(false);
        
    }

    public void DestroyBonus()
    {
        Destroy(gameObject);
    }

    public void SetColor()
    {
        sp2D.color = new Color(1f, 1f, 1f);
    }
}
