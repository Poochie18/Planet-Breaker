using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Angle to limit the rotation of the gun
    [SerializeField] private float limitAngle = 90f;
    [SerializeField] private float gunSize = 0.3f;

    private Vector3 mousePosition;

    public bool canRotate = true;

    private void Start()
    {
        //Sets the size of the cannon upon creation
        transform.localScale = new Vector2(1f, 1f) * gunSize;
    }
    void Update()
    {
        //Checking if the cannon can rotate
        if (!canRotate)
            RotateTheGun();
    }

    void RotateTheGun()
    {
        //Getting the cursor position relative to the coordinates of the world
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Obtaining the angle of rotation relative to the position of the gun and the position of the mouse
        var angle = Vector2.Angle(Vector2.down, mousePosition - transform.position);

        //Limiting the rotation of the cannon by an angle
        var realAngle = Mathf.Clamp(angle, -limitAngle, limitAngle);

        //Rotate the cannon at a given angle
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.x < mousePosition.x ? realAngle : -realAngle);
    }

    //Positioning the ball when fired from a cannon
    public void BallAddForce(Ball ball)
    {
       ball.Force(-transform.up);
    }

}