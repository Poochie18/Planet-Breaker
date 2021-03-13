using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float limitAngle = 90f;
    [SerializeField] private float gunSize;

    public bool rotation = true;

    private Vector3 mousePosition;

    private void Start()
    {
        transform.localScale = new Vector2(1f, 1f) * gunSize;
    }
    void Update()
    {
        if (!rotation)
            Rotation();
    }

    void Rotation()
    {
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = Vector2.Angle(Vector2.down, mousePosition - transform.position);

        var realAngle = Mathf.Clamp(angle, -limitAngle, limitAngle); 

        transform.eulerAngles = new Vector3(0f, 0f, transform.position.x < mousePosition.x ? realAngle : -realAngle);

        Debug.DrawLine(transform.position, mousePosition, Color.yellow);
    }

   public void BallAddForce(Ball ball)
   {
       ball.Force(-transform.up);
   }

}