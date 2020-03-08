using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SimplePlane : Plane
{
    //SIMPLE
    //private void Update()
    //{
    //    Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
    //    transform.position += Movement * thrust * Time.deltaTime;
    //}

    //ACTUAL THRUST
    private void Update()
    {
        Vector2 Movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Move(Movement);
        currentVelocity = (Mathf.Abs(rigidbody2D.velocity.x) + Mathf.Abs(rigidbody2D.velocity.y));
        Debug.Log("rigidbody2D.velocity: " + currentVelocity);
    }

    public override void Move(Vector2 direction)
    {
        if (currentVelocity <= maxVelocity)
        {
            rigidbody2D.AddForce(direction * acceleration);
        }
    }
}
