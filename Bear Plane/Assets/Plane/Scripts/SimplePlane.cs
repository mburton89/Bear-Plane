using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SimplePlane : Plane
{
    private bool _isThrusting;
    private int _x;
    private int _y;
    private Vector2 _previousVelocity;
    private float _absCurrentVelocity;

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
        _previousVelocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y);
        _absCurrentVelocity = (Mathf.Abs(rigidbody2D.velocity.x) + Mathf.Abs(rigidbody2D.velocity.y));
        //Debug.Log("rigidbody2D.velocity: " + currentVelocity);

        //print(_absCurrentVelocity);

        if (Input.anyKey)
        {
            _isThrusting = false;
            rigidbody2D.drag = 0;
        }
        else
        {
            _isThrusting = true;
            rigidbody2D.drag = friction;
        }
    }

    public override void Move(Vector2 direction)
    {
        //if(_isThrusting)
        //{
        //    rigidbody2D.drag = 0;
        //}
        //else
        //{
        //    rigidbody2D.drag = friction;
        //}

        rigidbody2D.AddForce(direction * acceleration);
        if (_absCurrentVelocity >= maxVelocity)
        {
            rigidbody2D.velocity = _previousVelocity;
        }
    }

    void HandleInput()
    { 
        if(Input.GetKeyDown(KeyCode.A))
        { 
            
        }
    }
}
