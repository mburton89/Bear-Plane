using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Enemy")
    //    {
    //        collision.GetComponent<Plane>().Splode();
    //        GetComponent<Plane>().Splode();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerPlane>()) return;

        if (collision.GetComponent<Plane>() && enabled == true)
        {
            Plane collidingPlane = collision.GetComponent<Plane>();
            if (collidingPlane.enabled == true)
            {
                if (!collidingPlane.GetComponent<PlayerPlane>())
                {
                    collidingPlane.Splode();
                }
                GetComponent<Plane>().Splode();
            }
        }
    }

    //public override void Move(Vector2 force)
    //{
    //    throw new System.NotImplementedException();
    //}
}
