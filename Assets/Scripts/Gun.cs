using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float ballForce = 12f;
    [SerializeField] private float limitAngle = 90f;

    public bool rotation = true;

    private Vector3 mousePosition;
    void Update()
    {
        if (rotation)
            Rotation();
    }

    void Rotation()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var angle = Vector2.Angle(Vector2.down, mousePosition - transform.position);

        var realAngle = Mathf.Clamp(angle, -limitAngle, limitAngle); 

        transform.eulerAngles = new Vector3(0f, 0f, transform.position.x < mousePosition.x ? realAngle : -realAngle);

        Debug.DrawLine(transform.position, mousePosition, Color.yellow);
    }

   public void BallAddForce(Ball ball)
   {
       ball.Force(-transform.up);
       //ball.rb.AddForce(-transform.up * ballForce);
   }

}