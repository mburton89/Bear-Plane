using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plane : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public int maxArmor;
    public float projectileSpeed;
    [HideInInspector] public int currentArmor;
    [HideInInspector] public Rigidbody2D rigidbody2D;
    [SerializeField] private ThrustParticle _thrustParticlePrefab;
    [SerializeField] private Projectile _projectilePrefab;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rigidbody2D.velocity.magnitude > maxSpeed)
        {
            rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxSpeed;
        }
    }

    public abstract void Move(Vector2 force);

    public void CreateThrustParticle()
    {
        float offsetX = Random.Range(-.2f, .2f);
        float offsetY = Random.Range(-.2f, .2f);
        Vector3 currentPos = this.transform.position;
        Vector3 spawnPos = new Vector3(currentPos.x + offsetX - .1f, currentPos.y + offsetY, 0);
        ThrustParticle thrustParticle = Instantiate(_thrustParticlePrefab, spawnPos, this.transform.rotation, this.transform);
    }

    public void FireProjectile(Vector2 direction)
    {
        Projectile projectile = Instantiate(_projectilePrefab, this.transform.position, this.transform.rotation, this.transform);
        projectile.rigidbody2D.AddForce(direction * projectileSpeed);
    }
}
