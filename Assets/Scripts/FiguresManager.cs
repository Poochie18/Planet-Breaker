using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresManager : MonoBehaviour
{
    [SerializeField] private Figures[] ship;
    [SerializeField] private Bonus bonus;

    [SerializeField] private GunManager gunManager;

    [SerializeField] private float stepPlacing = 0.7f;
    [SerializeField] private float maxLeftPosition = -1f;
    [SerializeField] private float maxRightPosition = 0.8f;
    [SerializeField] private float moveScale = 0.5f;
    
    private float maxPosition;

    [SerializeField] private int levelCoefficient = 4;
    [SerializeField] private int hpMultiplayer = 6;

    private List<Figures> figuresList = new List<Figures>();
    private List<Bonus> bonusesList = new List<Bonus>();

    void Awake()
    {
        GameManager.OnAfterAction += MovingFigures;
        GameManager.OnAfterAction += MovingBonuses;

        GameManager.OnStartGame += MovingFigures;
        GameManager.OnStartGame += MovingBonuses;
    }

    public void PlacingFigures()
    {
        for(float i = maxLeftPosition; i < maxRightPosition; i += stepPlacing)
        {
            Vector2 position = new Vector2(i, Random.Range(-1.4f, -1.2f));
            if (Random.Range(0, 2) == 1)
            {
                int figureNumber = Random.Range(0, ship.Length);
                
                Figures newFig = Instantiate(ship[figureNumber], position, Quaternion.Euler(0, 0, 180));

                int hp = CountingHelthPoints();
                newFig.SetHelthPoints(hp);

                figuresList.Add(newFig);

            }else if(Random.Range(0, 5) == 1)
            {
                var newBonus = Instantiate(bonus, position, Quaternion.Euler(0, 0, 180));
                newBonus.SetGunManager(gunManager);
                bonusesList.Add(newBonus);
            }
        }
    }

    private int CountingHelthPoints()
    {
        int coef = (int)Mathf.Floor(GameManager.GetCurrentLevel() / levelCoefficient);
        int hp = Random.Range((hpMultiplayer * coef + coef) + 1, hpMultiplayer * (coef + 1) + coef) ;
        return hp;
    }

    public void MovingFigures()
    {
        float temp = float.MinValue;
        for(int i = figuresList.Count - 1; i >= 0; i--)
        {
            var figura = figuresList[i];
            if(!figura.IsAlive)
            {
                figuresList.RemoveAt(i);
                figura.DestroyFigure();
                continue;
            }
            Vector2 figurePosition = figura.transform.position;
            figura.transform.position = new Vector2(figurePosition.x, figurePosition.y + moveScale);
            if (figura.transform.position.y >= temp)
                temp = figura.transform.position.y;
        }
        maxPosition = temp;
    }

    public void MovingBonuses()
    {
        for (int i = bonusesList.Count - 1; i >= 0; i--)
        {
            var bonus = bonusesList[i];
            if (!bonus.IsAlive)
            {
                bonusesList.RemoveAt(i);
                bonus.DestroyBonus();
            }
            else
            {
                Vector2 bonusPosition = bonus.transform.position;
                bonus.transform.position = new Vector2(bonusPosition.x, bonusPosition.y + 0.5f);
                if (bonus.transform.position.y >= maxPosition) bonus.DestroyBonus();
            }
        }
    }

    public void DestroyAllFigures()
    {
        foreach(Figures fig in figuresList)
            fig.DestroyFigure();
        figuresList.Clear();

        foreach (Bonus bonus in bonusesList)
            bonus.DestroyBonus();
        bonusesList.Clear();
    }

    public float GetMaxtPosition() { return maxPosition; }

    void OnDestroy()
    {
        GameManager.OnAfterAction -= MovingFigures;
        GameManager.OnAfterAction -= MovingBonuses;

        GameManager.OnStartGame -= MovingFigures;
        GameManager.OnStartGame -= MovingBonuses;
    }
}
