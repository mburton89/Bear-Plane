﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    public static ScreenShaker Instance;
    [SerializeField] private float _duration;
    [SerializeField] private float _intensity;
    private bool _canShake;

    private void Awake()
    {
        Instance = this;
        _canShake = false;
    }

    private void Update()
    {
        if(_canShake)
        {
            float _x = 0;
            float _y = 0;
            _x = Random.Range(-_intensity, _intensity);
            _y = Random.Range(-_intensity, _intensity);
            print(_x + ", " + _y);
            Vector3 newCameraPos = new Vector3(_x, _y, -10);
            transform.position = newCameraPos;
        }
    }

    public void ShakeScreen()
    {
        StartCoroutine(shakeScreenCo());
    }

    IEnumerator shakeScreenCo()
    {
        _canShake = true;
        yield return new WaitForSeconds(_duration);
        _canShake = false;
        transform.position = new Vector3(0, 0, -10);
    }
}