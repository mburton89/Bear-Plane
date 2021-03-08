using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plane : MonoBehaviour
{
    public float acceleration;
    public float maxVelocity;
    public float friction;
    public int armor;

    [HideInInspector] public float currentVelocity;
    [HideInInspector] public Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

<<<<<<< Updated upstream
    public abstract void Move(Vector2 force);
=======
    public void Splode()
    {
        if(hasPilot)
        {
            LaunchPilot(40);
        }

        if (tag == "Enemy")
        {
            BearPlaneStateManager.Instance.AddEnergy(energyToGivePlayer);
            ScoreManager.Instance.IncrementScore();
        }
        Instantiate(_explosionPrefab, this.transform.position, this.transform.rotation);
        Destroy(gameObject);

        if (GetComponent<BearPlaneStateManager>())
        {
            DeathManager.Instance.HandleDeath();
        }
    }

    public void HandleHit(int damageTaken)
    {
        currentArmor = currentArmor - damageTaken;

        if (currentArmor > 0)
        {
            if (currentArmor == 1)
            {
                rigidBody2D.freezeRotation = false;
                isToast = true;
                if (GetComponent<PlaneAI>())
                {
                    GetComponent<PlaneAI>().enabled = false;
                }
            }
            PlayHitSound();
        }
        else
        {
            Splode();
        }
    }



    public void PlayHitSound()
    {
        _hitAudioSource.Play();
    }

    public void LaunchPilot(int throwSpeed)
    {
        Projectile pilot = Instantiate(_pilotPrefab, this.transform.position, this.transform.rotation, null);
        pilot.Init(this.gameObject);
        pilot.GetComponent<Rigidbody2D>().AddForce(new Vector3(-0.5f, 1, 0) * throwSpeed);

        if (GetComponent<PlaneAI>())
        {
            GetComponent<PlaneAI>().enabled = false;
        }
    }

    IEnumerator fireRateBuffer()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    //Directional Splosions
    public void HandleHit(int damageTaken, Vector3 directionDamageCameFrom)
    {
        currentArmor = currentArmor - damageTaken;

        if (currentArmor > 0)
        {
            if (currentArmor == 1)
            {
                rigidBody2D.freezeRotation = false;
                isToast = true;
                if (GetComponent<PlaneAI>())
                {
                    GetComponent<PlaneAI>().enabled = false;
                }
            }
            PlayHitSound();
        }
        else
        {
            Splode(directionDamageCameFrom);
        }
    }

    public void Splode(Vector3 directionDamageCameFrom)
    {
        if (hasPilot)
        {
            LaunchPilot(40, directionDamageCameFrom);
        }

        if (tag == "Enemy")
        {
            BearPlaneStateManager.Instance.AddEnergy(energyToGivePlayer);
            ScoreManager.Instance.IncrementScore();
        }
        Instantiate(_explosionPrefab, this.transform.position, this.transform.rotation);
        Destroy(gameObject);

        if (GetComponent<BearPlaneStateManager>())
        {
            DeathManager.Instance.HandleDeath();
        }
    }

    public void LaunchPilot(int throwSpeed, Vector3 direction)
    {
        Projectile pilot = Instantiate(_pilotPrefab, this.transform.position, this.transform.rotation, null);
        pilot.Init(this.gameObject);
        pilot.GetComponent<Rigidbody2D>().AddForce(direction * throwSpeed);

        if (GetComponent<PlaneAI>())
        {
            GetComponent<PlaneAI>().enabled = false;
        }
    }
>>>>>>> Stashed changes
}
