
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] private Gun gunPrefab;
    [SerializeField] private Ball ballPrefab;
    //Delay in the release of the ball from the cannon
    [SerializeField] private float shootDelay = 0.6f;

    //Creating a queue for storing balls
    private Queue<Ball> balls = new Queue<Ball>();

    //Instance cannon
    private Gun gun;

    //Gun installation position
    private Vector2 mainPosition;

    //Variable for storing the total number of balls in the game
    public int ball_count = 1;

    private void Start()
    {
        GameManager.OnClick += ShootingTheBall;

        //Setting the current position
        mainPosition = transform.position;

        //Creating a new instance of a cannon on the field
        gun = Instantiate(gunPrefab, mainPosition, Quaternion.identity);

        //Creating and adding new balls to the cannon queue
        for (int i = 0; i < ball_count; i++)
        {
            Ball ball = Instantiate(ballPrefab, mainPosition, Quaternion.identity);
            AddToQueue(ball);
        }
        
    }

    //Returns true when the number of balls in the cannon collector 
    //is equal to the value of balls in the game
    public bool CheckBallsCount() { return balls.Count == ball_count; }

    //Sends a spin value to the cannon prefab
    public void FreezRotation(bool setfreez) {gun.canRotate = setfreez;}

    public void InstantiateBall(Vector2 spawnPos)
    {
        //A new ball is created at the position that is passed to the method
        Ball ball = Instantiate(ballPrefab, spawnPos, Quaternion.identity);

        //Once created, force is given to the ball in a random direction.
        ball.Force(new Vector2(Random.Range(-1, 1f), Random.Range(0, 1f)) * 1);

        //The total number of balls in the game is replenished
        ball_count += 1;
        
    }

    private void AddToQueue(Ball ball)
    {
        //When the ball enters the queue, it becomes inactive
        ball.SetVisibility(false);

        //The position is set in the position of the gun
        ball.SetPosition(mainPosition);

        //Add directly to the queue
        balls.Enqueue(ball);
    }
    
    private void ShootingTheBall()
    {
        StartCoroutine(WaitSecond());
    }

    //Method starts on event OnClick
    IEnumerator WaitSecond()
    {
        //We go through the entire line of balls
        int tempCount = balls.Count;
        for (int i = 0; i < tempCount; i++)
        {
            //Removing balls from the queue
            var ball = balls.Dequeue();

            //Setting the visibility of the ball
            ball.SetVisibility(true);

            //Adding strength to simulate a shot
            gun.BallAddForce(ball);

            //How to Create a Delay Between Cannon Balls
            yield return new WaitForSeconds(shootDelay);
        }
    }

    //When the ball touches the collector trigger, the ball is transferred to the queue
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Ball ball = coll.gameObject.GetComponent<Ball>();
        AddToQueue(ball);
    }

    private void OnDestroy()
    {
        GameManager.OnClick -= ShootingTheBall;
    }
}
