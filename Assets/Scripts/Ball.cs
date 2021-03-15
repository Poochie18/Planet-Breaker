using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //The value with what force the ball flies
    [SerializeField] private float ballForce = 100;
    [SerializeField] private float BallSize = 0.05f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D bx;

    void Start()
    {
        //Sets the size of the ball upon creation
        transform.localScale = new Vector2(1f, 1f) * BallSize;
    }

    //Setting the visibility value for the ball
    public void SetVisibility(bool isVisible){ gameObject.SetActive(isVisible); }

    //Positioning the ball relative to the position of the gun
    public void SetPosition(Vector3 gunPosition) { transform.position = gunPosition; }

    //Giving the ball the power of the shot relative to the position of the gun and the value of the force
    public void Force(Vector3 position){ rb.AddForce(position * ballForce); }


    //Tracking the contact of the ball with the floor 
    //to give the ball the force of movement towards the collector
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Elevator"))
        {
            var rig2D = gameObject.GetComponent<Rigidbody2D>();
            rig2D.constraints = RigidbodyConstraints2D.None;
            rig2D.velocity = new Vector2(4f, 0);
        }
    }
}
