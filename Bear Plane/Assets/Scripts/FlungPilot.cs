﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlungPilot : Projectile
{
    [SerializeField] private GameObject _bloodPrefab;
    float windForce;
    Vector2 windForceVector2;
    [HideInInspector] public bool canSplode;

    private void Start()
    {
        canSplode = false;
        StartCoroutine(SplodeBuffer());
        Destroy(gameObject, 3);
    }

    void FixedUpdate()
    {
        rigidbody2D.rotation += 6f;

        windForce -= 0.02f;
        windForceVector2 = new Vector2(windForce, 0);
        rigidbody2D.AddForce(windForceVector2 * 2);
    }

    public override void Splode()
    {
        Instantiate(_bloodPrefab, this.transform.position, this.transform.rotation);
        Destroy(gameObject);
    }

    private IEnumerator SplodeBuffer()
    {
        yield return new WaitForSeconds(.25f);
        canSplode = true;
    }
}
