using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float MaxMoveRight = 10f;
    public float MaxMoveLeft = 10f;
    public float WaitForSecondsBetweenSteps = 0.2f;
    public float MoveForce = 0.01f;

    private int _movingRight = 1;

    private float _startX;

    void Start()
    {
        _startX = transform.position.x;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + _movingRight * MoveForce, transform.position.y,
            transform.position.z);

        if (_movingRight == 1 && transform.position.x >= _startX + MaxMoveRight
            || _movingRight != 1 && transform.position.x <= _startX - MaxMoveLeft)
        {
            _movingRight *= -1;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        other.transform.parent = transform;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        other.transform.parent = null;
    }
}