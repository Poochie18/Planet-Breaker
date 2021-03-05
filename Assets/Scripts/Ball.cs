using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float ballForce = 100;
    [SerializeField] private float BallSize = 0.05f;

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private CircleCollider2D bx;

    public GunManager gunManager;

    void Start()
    {
        transform.localScale = new Vector3(1f, 1f, 1f) * BallSize;
    }

    public void SetVisibility(bool isVisible){
        gameObject.SetActive(isVisible); 
    }

    public void SetPosition(Vector3 gunPosition) { 
        transform.position = gunPosition;
    }

    public void Force(Vector3 position)
    {
        rb.AddForce(position * ballForce);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bottom"))
        {
            var rig2D = gameObject.GetComponent<Rigidbody2D>();
            rig2D.constraints = RigidbodyConstraints2D.None;
            rig2D.velocity = new Vector3(4f, 0, 0);
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Elevator"))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 4f, 0);
        }
    }
}
