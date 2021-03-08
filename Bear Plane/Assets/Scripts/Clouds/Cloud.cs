using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float _minX;
    private float _movementSpeed;
    public SpriteRenderer spriteRenderer;

    public void Init(float minX, float movementSpeed, int spriteOrder)
    {
        _minX = minX;
        _movementSpeed = movementSpeed;
        spriteRenderer.sortingOrder = spriteOrder;

        if (spriteOrder > 0)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, .75f);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.left * _movementSpeed * Time.deltaTime);
        CheckBoundaries();
    }

    void CheckBoundaries()
    {
        if (transform.position.x < _minX)
        {
            Destroy(gameObject);
        }
    }
}
