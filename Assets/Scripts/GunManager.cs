
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] private Gun gunPrefab;
    [SerializeField] private Ball ballPrefab;

    private Queue<Ball> balls = new Queue<Ball>();
    private Gun gun;
    private Vector3 mainPosition;

    public int ball_count;

    void Start()
    {
        GameManager.OnClick += Shooting;

        mainPosition = transform.position;
        gun = Instantiate(gunPrefab, mainPosition, Quaternion.identity);
        for(int i = 0; i < ball_count; i++)
        {
            Ball ball = Instantiate(ballPrefab, mainPosition, Quaternion.identity);
            AddToQueue(ball);
        }
        
    }

    public bool CheckBallsCount() { return balls.Count == ball_count; } 

    public void FreezRotation(bool setfreez) {gun.rotation = setfreez;}

    public void InstantiateBall(Vector3 spawnPos)
    {

        Ball ball = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        ball.Force(new Vector3(0.5f, 0.5f, 0f) * 1);
        ball_count += 1;
        
    }

    public void AddToQueue(Ball ball)
    {
        ball.SetVisibility(false);
        ball.SetPosition(mainPosition);
        balls.Enqueue(ball);
    }
    
    public void Shooting()
    {
        StartCoroutine(WaitSecond());
    }

    IEnumerator WaitSecond()
    {
        int tempCount = balls.Count;
        for (int i = 0; i < tempCount; i++)
        {
            var ball = balls.Dequeue();
            ball.SetVisibility(true);
            gun.BallAddForce(ball);
            yield return new WaitForSeconds(.06f);
        }
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        Ball ball = coll.gameObject.GetComponent<Ball>();
        AddToQueue(ball);
    }

    void OnDestroy()
    {
        GameManager.OnClick -= Shooting;
    }
}
