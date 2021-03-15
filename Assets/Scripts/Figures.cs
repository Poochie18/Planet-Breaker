using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Figures : MonoBehaviour
{
    [SerializeField] private int figureHealth;
    [SerializeField] private float figureSize = 0.06f;

    //The value of the force when the ball rebounds from the figure
    [SerializeField] private float  ballRepulsion = 40f;

    [SerializeField] private SpriteRenderer sp2D;

    public bool IsAlive => figureHealth > 0;

    //Color dictionary for shapes at different HP values
    private Dictionary<int, Color> dict = new Dictionary<int, Color>
    {
        { 0, new Color(1f, 1f, 0f) },
        { 1, new Color(0f, 1f, 0f) },
        { 2, new Color(0f, 1f, 1f) },
        { 3, new Color(0f, 0f, 1f) },
        { 4, new Color(1f, 0f, 1f) },
        { 5, new Color(1f, 0f, 0f) }
    };

    private void Start()
    {
        //Sets the size of the figure upon creation
        transform.localScale = new Vector2(1f, 1f) * figureSize;
    }

    public void DestroyFigure() { Destroy(gameObject);}

    //setting the HP value for the shape and color, relative to the HP value
    public void SetHelthPoints(int hp)
    {
        figureHealth = hp;
        SetColor();
    }

    //When the figure collides with the ball
    private void OnCollisionEnter2D(Collision2D col)
    {
        //The value is added to the total score
        GameManager.AddScore();

        //The figure loses 1 HP value
        figureHealth -= 1;

        //And if the figure has less or 0 HP, then it is marked as inactive
        if (!IsAlive) gameObject.SetActive(false);

        //If the figure is alive, then the HP value for the color change is checked
        SetColor();

        //Added strength to the ball after contact
        PushBallUp(col);
    }

    private void PushBallUp(Collision2D col)
    {
        //Creation of an impulse to the ball with a random vector, to give randomness to what is happening
        col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1f), Random.Range(0, 1f)) * ballRepulsion);
    }

    //Changing the color of a shape relative to its HP
    private void SetColor()
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