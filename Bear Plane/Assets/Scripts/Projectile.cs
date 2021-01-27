using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    private GameObject _firer;
    public int damageToGive;

    private void Start()
    {
        Destroy(gameObject, 3);
    }

    public void Init(GameObject firer)
    {
        this._firer = firer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision.gameObject != _firer)
        {
            collision.GetComponent<Plane>().HandleHit(damageToGive);
            if(collision.GetComponent<Plane>().hasPilot)
            {
                collision.GetComponent<Plane>().LaunchPilot(40);
            }
            Splode();
        }

        if (collision.tag == "Player" && collision.gameObject != _firer && !GetComponent<FlungPilot>())
        {
            collision.GetComponent<BearPlaneStateManager>().HandleHit(damageToGive);
            Splode();
        }
    }

    public abstract void Splode();
}
