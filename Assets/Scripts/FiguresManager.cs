using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresManager : MonoBehaviour
{
    //Array for storing figure types
    [SerializeField] private Figures[] planets;

    //Bonus prefab
    [SerializeField] private Bonus bonus;

    //Cannon Manager Prefab
    [SerializeField] private GunManager gunManager;

    //Step to place the figure
    [SerializeField] private float stepPlacing = 0.7f;

    //Left border of figure placement
    [SerializeField] private float maxLeftPosition = -1f;
    //Right border of figure placement
    [SerializeField] private float maxRightPosition = 0.8f;
    //Bottom position of the figure
    [SerializeField] private float minDownPosition = -3f;
    //Values for moving figures up
    [SerializeField] private float moveScale = 0.5f;

    //The coefficient of the number of levels after which the number of HP of figures increases
    [SerializeField] private int levelCoefficient = 4;

    //The coefficient of what value on average increases the value of the HP of the figures
    [SerializeField] private int hpMultiplayer = 6;

    //Highest position of tallest figure
    private float maxPosition;

    //Lists for storing created figures and bonuses
    private List<Figures> figuresList = new List<Figures>();
    private List<Bonus> bonusesList = new List<Bonus>();

    private void Awake()
    {
        //Moving pieces and bonuses after the end of the action on the field
        GameManager.OnAfterAction += MovingFigures;
        GameManager.OnAfterAction += MovingBonuses;

        //Create a new line of figures
        GameManager.PlaceNewFigures += PlacingFigures;

        //Moving figures and bonuses at the start of the game
        GameManager.OnStartGame += MovingFigures;
        GameManager.OnStartGame += MovingBonuses;

        //Loss event
        GameManager.OnLoseGame += DestroyAllFigures;
    }


    private void PlacingFigures()
    {
        //Take the maximum left possible position and move in increments 
        //to the maximum right position to create new figures
        for (float i = maxLeftPosition; i < maxRightPosition; i += stepPlacing)
        {
            //A new position is created with an i-value on the X-axis and a minDownPosition value on the Y-axis with a slight deviation
            Vector2 position = new Vector2(i, Random.Range(minDownPosition - 0.1f, minDownPosition + 0.1f));
            
            //50 / 50 is selected, whether the figure will be here or not
            if (Random.Range(0, 2) == 1)
            {
                //Selects a random figure value from the list of all shapes
                int figureNumber = Random.Range(0, planets.Length);

                //A new figure is created on the field with the position calculated above and the selected prefab
                var newFig = Instantiate(planets[figureNumber], position, Quaternion.Euler(0, 0, 180));

                //The HP value is calculated and set for the corresponding level of the game
                int hp = CountingHelthPoints();
                newFig.SetHelthPoints(hp);

                //The figure is added to the list of all figures
                figuresList.Add(newFig);

            //If the place is empty, then a new bonus is created with a probability of 1/6
            }
            else if(Random.Range(0, 5) == 1)
            {
                //A new bonus is created on the field with the position calculated above and the selected prefab
                var newBonus = Instantiate(bonus, position, Quaternion.Euler(0, 0, 180));

                //Sets the current cannon, which will own the balls created by the bonus
                newBonus.SetGunManager(gunManager);

                //The bonus is added to the list of all bonuses
                bonusesList.Add(newBonus);
            }
        }
    }

    //Secret formula
    private int CountingHelthPoints()
    {
        int coef = (int)Mathf.Floor(GameManager.GetCurrentLevel() / levelCoefficient);
        int hp = Random.Range((hpMultiplayer * coef + coef) + 1, hpMultiplayer * (coef + 1) + coef) ;
        return hp;
    }

    //Moving and checking shapes for activity
    private void MovingFigures()
    {
        //Create the current minimum position
        float temp = float.MinValue;

        //We go through the entire list of figures
        for (int i = figuresList.Count - 1; i >= 0; i--)
        {
            var figura = figuresList[i];

            //For each figure, we check whether it is active or not.
            if (!figura.IsAlive)
            {
                //Remove from the list and destroy
                RemoveFigureFromList(i, figura);
            }
            else
            {
                //Move the figure to a new position by value moveScale
                Vector2 figurePosition = figura.transform.position;
                figura.transform.position = new Vector2(figurePosition.x, figurePosition.y + moveScale);

                //We calculate the new maximum value of the position of the figures
                temp = CalculateMaxFigurePosition(temp, figurePosition);
            }
        }
        //We remember the new maximum value of the position of the figures
        maxPosition = temp;
    }

    private void RemoveFigureFromList(int i, Figures figura)
    {
        ////Remove from the list and destroy
        figuresList.RemoveAt(i);
        figura.DestroyFigure();
    }

    //If the value of the figure's position along the Y-axis is greater than the current maximum value, then set this value as the maximum
    private float CalculateMaxFigurePosition(float temp, Vector2 figurePosition)
    {
        return (figurePosition.y >= temp) ? figurePosition.y : temp;
    }

    private void MovingBonuses()
    {
        //We go through the entire list of bonuses
        for (int i = bonusesList.Count - 1; i >= 0; i--)
        {
            var bonus = bonusesList[i];
            //For each bonus, we check whether it is active or not.
            if (!bonus.IsAlive)
            {
                //Remove from the list and destroy
                RemoveBonusFromList(i, bonus);
            }
            else
            {
                //Move the bonus to a new position by value moveScale
                Vector2 bonusPosition = bonus.transform.position;
                bonus.transform.position = new Vector2(bonusPosition.x, bonusPosition.y + 0.5f);
            }
        }
    }

    private void RemoveBonusFromList(int i, Bonus bonus)
    {
        //Remove from the list and destroy
        bonusesList.RemoveAt(i);
        bonus.DestroyBonus();
    }

    //For all objects from the lists of figures and bonuses, delete objects and clear the lists
    private void DestroyAllFigures()
    {
        foreach(Figures fig in figuresList)
            fig.DestroyFigure();
        figuresList.Clear();

        foreach (Bonus bonus in bonusesList)
            bonus.DestroyBonus();
        bonusesList.Clear();
    }

    public float GetMaxtPosition() { return maxPosition; }

    private void OnDestroy()
    {
        GameManager.OnAfterAction -= MovingFigures;
        GameManager.OnAfterAction -= MovingBonuses;

        GameManager.PlaceNewFigures -= PlacingFigures;

        GameManager.OnStartGame -= MovingFigures;
        GameManager.OnStartGame -= MovingBonuses;
    }
}
