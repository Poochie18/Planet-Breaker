using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresManager : MonoBehaviour
{
    [SerializeField] private Figures[] fig;
    [SerializeField] private Bonus bonus;
    [SerializeField] private float leftWall = -0.85f;
    [SerializeField] private float rightWall = 0.85f;
    [SerializeField] private float stepPlacing = 0.45f;

    private List<Figures> figuresList = new List<Figures>();
    private List<Bonus> bonusesList = new List<Bonus>();

    void Awake()
    {
        GameManager.OnAfterAction += MovingFigures;
    }

    public void PlacingFigures(GunManager gunManager)
    {
        for(float i = leftWall; i < rightWall; i += stepPlacing)
        {
            if(Random.Range(0, 2) == 1)
            {
                int figureNumber = Random.Range(0, fig.Length);
                Vector3 figurePosition = new Vector3(i, Random.Range(-1.4f, -1f), -2f);
                figuresList.Add(Instantiate(fig[figureNumber], figurePosition, Quaternion.Euler(0, 0, 180)));
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
            if(figura.IsAlive)
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
