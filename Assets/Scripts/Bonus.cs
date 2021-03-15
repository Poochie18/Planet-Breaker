using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bonus : MonoBehaviour
{
    [SerializeField] private float bonusSize = 0.06f;
    [SerializeField] private int bonusHP;

    [SerializeField] private SpriteRenderer sp2D;

    private GunManager gunManager;

    public bool IsAlive => bonusHP > 0;
    
    private void Start()
    {
        //Sets the size of the bonus upon creation
        transform.localScale = new Vector2(1f, 1f) * bonusSize;
    }

    //Adding a cannon object to further add balls to the cannon queue
    public void SetGunManager(GunManager gun) { gunManager = gun; }

    public void DestroyBonus() { Destroy(gameObject); }

    //When the ball and the bonus touch
    private void OnCollisionEnter2D(Collision2D col)
    {
        //A new ball is created
        CreatingNewBall(col);

        //The bonus HP value is set to 0
        bonusHP = 0;

        //The bonus becomes inactive and is marked for deletion
        gameObject.SetActive(false);
        
    }

    //A new ball is created for the cannon object with the position of the touched bonus
    private void CreatingNewBall(Collision2D col)
    {
        gunManager.InstantiateBall(col.gameObject.transform.position);
    }
}
