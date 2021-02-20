using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresManager : MonoBehaviour
{
    [SerializeField] private Figures[] ship;
    [SerializeField] private Bonus bonus;
    [SerializeField] private float stepPlacing = 0.7f;
    [SerializeField] private float maxLeftPosition = -1f;
    [SerializeField] private float maxRightPosition = 0.8f;
    private int levelCoefficient = 4;

    private List<Figures> figuresList = new List<Figures>();
    private List<Bonus> bonusesList = new List<Bonus>();

    void Awake()
    {
        GameManager.OnAfterAction += MovingFigures;
    }

    public void PlacingFigures(GunManager gunManager)
    {
        for(float i = maxLeftPosition; i < maxRightPosition; i += stepPlacing)
        {
            if(Random.Range(0, 2) == 1)
            {
                int figureNumber = Random.Range(0, ship.Length);
                Vector3 figurePosition = new Vector3(i, Random.Range(-1.4f, -1f), -2f);
                Figures newFig = Instantiate(ship[figureNumber], figurePosition, Quaternion.Euler(0, 0, 180));
                int coefForMiltiple = (int)Mathf.Floor(GameManager.GetCurrentLevel() / levelCoefficient);
                int hp = Random.Range(1 + coefForMiltiple * 5, 5 + coefForMiltiple * 5) ;
                newFig.SetHelthPoints(hp);
                figuresList.Add(newFig);
            }
        }

        /*
        Vector2 bonusPosition = new Vector2(Random.Range(-2f, 1.5f), -1f);
        var newBonus = Instantiate(bonus, bonusPosition, Quaternion.identity);
        listOfBonuses.Add(newBonus);
        newBonus.gunManager = gunManager;*/
    }

    public void MovingFigures()
    {
        for(int i = figuresList.Count - 1; i >= 0; i--)
        {
            var figura = figuresList[i];
            if(!figura.IsAlive)
            {
                figuresList.RemoveAt(i);
                figura.DestroyFigure();
            }
            Vector3 figurePosition = figura.transform.position;
            figura.transform.position = new Vector3(figurePosition.x, figurePosition.y + 1, -2f);
        }
    }

    void OnDestroy()
    {
        GameManager.OnAfterAction -= MovingFigures;
    }
}
