using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating(nameof(CheckBoundaries), 0, 2);   
    }

    void CheckBoundaries()
    {
        if (transform.position.x < -100)
        {
            Destroy(gameObject);
        }
    }
}
