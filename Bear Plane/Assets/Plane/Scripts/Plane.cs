using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plane : MonoBehaviour
{
    public float acceleration;
    public float maxVelocity;
    public int armor;

    [HideInInspector] public float currentVelocity;
    [HideInInspector] public Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public abstract void Move(Vector2 force);
}
