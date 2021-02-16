using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figures : MonoBehaviour
{
    [SerializeField] private int figureHealth = 3;
    [SerializeField] private float figureSize = 1f;

    public bool IsAlive => figureHealth > 0;

    void Start()
    {
        transform.localScale = new Vector3(1f, 1f, 1f) * figureSize;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        figureHealth -= 1;
        if(figureHealth <= 0) gameObject.SetActive(false);
    }
    public void DestroyFigure()
    {
        Destroy(gameObject);
    }
}
