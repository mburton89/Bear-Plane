﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public int maxArmor;
    public float projectileSpeed;
    public float energyToGivePlayer;
    public float fireRate;
    [HideInInspector] public int currentArmor;
    [HideInInspector] public Rigidbody2D rigidBody2D;
    [SerializeField] private ThrustParticle _fullHealthThrustParticlePrefab;
    [SerializeField] private ThrustParticle _lowHealthThrustParticlePrefab;
    [SerializeField] private ThrustParticle _noHealthThrustParticlePrefab;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private AudioSource _gunAudioSource;
    [SerializeField] private AudioSource _hitAudioSource;
    public bool isControlling = false;

    [SerializeField] private FlungPilot _pilotPrefab;
    [HideInInspector] public bool hasPilot = true;
    [HideInInspector] public bool isToast = false;
    [HideInInspector] public bool canShoot = true;

    private bool _isEnemy;

    public void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentArmor = maxArmor;
        _isEnemy = GetComponent<EnemyPlane>();
    }

    void FixedUpdate()
    {
        if (PlayerSwap.isPlane && isControlling)
        {
            Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Move(direction);

            if (Input.GetMouseButtonDown(0))
            {
                FireProjectile(Vector2.right);
            }
        }

        CreateThrustParticles();

        if (_isEnemy && !hasPilot)
        {
            maxSpeed = 100;
            acceleration += 3f;
            Move(Vector3.left);
        } 

        if (rigidBody2D.velocity.magnitude > maxSpeed)
        {
            rigidBody2D.velocity = rigidBody2D.velocity.normalized * maxSpeed;
        }
    }

    public void Move(Vector2 direction)
    {
        if (rigidBody2D != null)
        {
            rigidBody2D.AddForce(direction * acceleration * Time.deltaTime);
        }
    }

    public void CreateThrustParticles()
    {
        float offsetX = Random.Range(-.2f, .2f);
        float offsetY = Random.Range(-.2f, .2f);
        Vector3 currentPos = this.transform.position;
        Vector3 spawnPos = new Vector3(currentPos.x + offsetX - .1f, currentPos.y + offsetY, 0);

        if (currentArmor > 1)
        {
            ThrustParticle thrustParticle = Instantiate(_fullHealthThrustParticlePrefab, spawnPos, this.transform.rotation, this.transform);
        }

        else
        {
            ThrustParticle thrustParticle = Instantiate(_noHealthThrustParticlePrefab, spawnPos, this.transform.rotation, this.transform);
            rigidBody2D.rotation += -0.2f;
            rigidBody2D.gravityScale += 0.006f;
        }
    }

    public void FireProjectile(Vector2 direction)
    {
        Projectile projectile = Instantiate(_projectilePrefab, this.transform.position, this.transform.rotation);
        projectile.Init(this.gameObject);
        projectile.rigidbody2D.AddForce(direction.normalized * projectileSpeed);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _gunAudioSource.Play();
        StartCoroutine(fireRateBuffer());
    }

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
            FuelGauge.Instance.AddUnitsOfFuel(5);
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
        if (!hasPilot) return;

        hasPilot = false;

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

            rigidBody2D.AddRelativeForce(directionDamageCameFrom * 1000);
            rigidBody2D.AddTorque(directionDamageCameFrom.x * 100);
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
        if (!hasPilot) return;

        hasPilot = false;

        Projectile pilot = Instantiate(_pilotPrefab, this.transform.position, this.transform.rotation, null);
        pilot.Init(this.gameObject);
        pilot.GetComponent<Rigidbody2D>().AddForce(direction * throwSpeed);

        if (GetComponent<PlaneAI>())
        {
            GetComponent<PlaneAI>().enabled = false;
        }
    }
}
