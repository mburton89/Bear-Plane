using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearClaws : MonoBehaviour
{
    private PlayerPlane _controller;
    public float energyConsumptionAmount;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private AudioSource _audioSource;
    public int damageToGive;

    public void Init(PlayerPlane controller)
    {
        _controller = controller;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Plane plane = collision.GetComponent<Plane>();
            plane.HandleHit(damageToGive);
            _audioSource.Play();
            ScreenShaker.Instance.ShakeScreen(0.1f, 0.2f);
            if (plane.hasPilot)
            {
                plane.LaunchPilot(40);
                plane.hasPilot = false;
                //plane.Splode();
            }
        }

        if (collision.GetComponent<FlungPilot>())
        {
            collision.GetComponent<FlungPilot>().Splode();
        }

        if (collision.GetComponent<Bullet>())
        {
            collision.GetComponent<Bullet>().Init(this.gameObject);
            collision.GetComponent<Rigidbody2D>().velocity = -collision.GetComponent<Rigidbody2D>().velocity * 1.5f;
        }
    }

    public void Attack()
    {
        _collider.enabled = true;
    }

    public void FinishAttack()
    {
        _collider.enabled = false;
    }
}
