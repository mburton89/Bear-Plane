using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerPlane : Plane
{
    private void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Move(direction);

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            FireProjectile(Vector2.right);
        }
    }

    public override void Move(Vector2 direction)
    {
         rigidbody2D.AddForce(direction * acceleration);
        if (direction != Vector2.zero)
        {
            CreateThrustParticle();
        }
    }
}
