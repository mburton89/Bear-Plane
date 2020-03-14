using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRight : MonoBehaviour
{
    private Plane _plane;

    void Awake()
    {
        _plane = GetComponent<Plane>();
    }

    void Update()
    {
        if (!_plane.isToast)
        {
            _plane.Move(Vector2.right);
        }
    }
}
