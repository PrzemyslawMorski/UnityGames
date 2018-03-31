using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class TrollController : MonoBehaviour
    {
        private Direction _direction;
        private Rigidbody2D _rigidbody2D;
        private Animator _anim;

        public Transform GroundCheck;
        public LayerMask WhatIsGround;

        public float MoveForce = 100f;
        public float WaitForSecondsBetweenSteps = 0.2f;
        public float MaxIdleWaitForSeconds = 2f;
        public float MinIdleWaitForSeconds = 0f;
        public float GroundRadius = 0.2f;

        private enum Direction
        {
            Left = -1,
            Right = 1
        };

        void Start()
        {
            _direction = Direction.Right;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            StartCoroutine(KeepMoving());
        }

        private IEnumerator IdleForARandomPeriod()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _anim.SetBool("Walking", false);

            var randomPeriod = Random.Range(MinIdleWaitForSeconds, MaxIdleWaitForSeconds);

            yield return new WaitForSeconds(randomPeriod);

            FlipHorizontally();
            StartCoroutine(KeepMoving());
        }

        private void FlipHorizontally()
        {
            _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;

            var theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }


        private IEnumerator KeepMoving()
        {
            _anim.SetBool("Walking", true);
            
            while (HasSpace())
            {
                if (_rigidbody2D.velocity == Vector2.zero)
                {
                    _rigidbody2D.AddForce(new Vector2(MoveForce, 0));
                }

                yield return new WaitForSeconds(WaitForSecondsBetweenSteps);
            }

            StartCoroutine(IdleForARandomPeriod());
        }

        private bool HasSpace()
        {
            bool result = Physics2D.OverlapCircle
                (GroundCheck.position, GroundRadius, WhatIsGround);

            return result;
        }

    }
}