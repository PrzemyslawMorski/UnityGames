using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float MaxMoveRight = 10f;
    public float MaxMoveLeft = 10f;
    public float WaitForSecondsBetweenSteps = 0.2f;
    public float MoveForce = 10f;

    private int _movingRight = 1;

    private float _startX;

    void Start ()
    {
        _startX = transform.position.x;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(_movingRight * MoveForce, 0));
    }

    void FixedUpdate()
    {
        if (_movingRight == 1 && transform.position.x >= _startX + MaxMoveRight
            || _movingRight != 1 && transform.position.x <= _startX - MaxMoveLeft)
        {
            ReverseMovingForce();
        }
    }

    private void ReverseMovingForce()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        _movingRight *= -1;

        GetComponent<Rigidbody2D>().AddForce(new Vector2(_movingRight * MoveForce, 0));
    }

}
