using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private int bonusSize = 3;

    public GunManager gunManager;

    void Start()
    {
        this.transform.localScale = new Vector3(1f, 1f, 1f) * bonusSize;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
        gunManager.InstantiateBall();
    }
}
