using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Figures : MonoBehaviour
{
    [SerializeField] private int figureHealth;
    [SerializeField] private float figureSize = 0.06f;

    [SerializeField] private SpriteRenderer sp2D;

    public bool IsAlive => figureHealth > 0;

    Dictionary<int, Color> dict = new Dictionary<int, Color>
    {
        { 0, new Color(1f, 1f, 0f) },
        { 1, new Color(0f, 1f, 0f) },
        { 2, new Color(0f, 1f, 1f) },
        { 3, new Color(0f, 0f, 1f) },
        { 4, new Color(1f, 0f, 1f) },
        { 5, new Color(1f, 0f, 0f) }
    };

    void Start()
    {
        transform.localScale = new Vector3(1f, 1f, 1f) * figureSize;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(0, 1f)) * 15f);

        GameManager.score += 1;
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
        int coef = Mathf.FloorToInt(figureHealth / 6);
        try
        {
            sp2D.color = dict[coef];
        }
        catch
        {
            sp2D.color = new Color(1f, 0f, 0f);
        }
        
    }

}