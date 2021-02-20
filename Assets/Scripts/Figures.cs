using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figures : MonoBehaviour
{
    [SerializeField] private int figureHealth;
    [SerializeField] private float figureSize = 0.07f;

    [SerializeField] private SpriteRenderer sp2D;

    public bool IsAlive => figureHealth > 0;

    void Start()
    {
        transform.localScale = new Vector3(1f, 1f, 1f) * figureSize;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        figureHealth -= 1;
        if(figureHealth <= 0) gameObject.SetActive(false);
        SetColor();
    }
    public void DestroyFigure()
    {
        Destroy(gameObject);
    }

    public void SetHelthPoints(int hp)
    {
        figureHealth = hp;
        SetColor();
    }

    public void SetColor()
    {
        if (figureHealth >= 1 && figureHealth < 6)
        {
            sp2D.color = new Color(1f, 1f, 0f);
        }
        else if (figureHealth >= 6 && figureHealth < 11)
        {
            sp2D.color = new Color(0f, 1f, 0f);
        }
        else if (figureHealth >= 11 && figureHealth < 30)
        {
            sp2D.color = new Color(0f, 1f, 1f);
        }
        else if (figureHealth >= 30 && figureHealth < 60)
        {
            sp2D.color = new Color(0f, 0f, 1f);
        }
        else if (figureHealth >= 61 && figureHealth < 100)
        {
            sp2D.color = new Color(1f, 0f, 1f);
        }
        else
        {
            sp2D.color = new Color(1f, 0f, 0f);
        }
    }
}
