using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class TrollController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Animator _anim;
        private bool _facingRight = true;

        public Transform GroundCheck;
        private float _groundCheckRadius = 0.2f;

        public LayerMask WhatIsGround;

        public float MoveForce = 100f;
        public float WaitForSecondsBetweenSteps = 0.2f;
        public float MaxIdleWaitForSeconds = 3f;
        public float MinIdleWaitForSeconds = 3f;

        void Start()
        {
            _facingRight = true;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            StartCoroutine(KeepMoving());
        }

        private IEnumerator IdleForARandomPeriod()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _anim.SetBool("Walking", false);

            var randomPeriod = Random.Range(MinIdleWaitForSeconds, MaxIdleWaitForSeconds);

            yield return new WaitForSeconds(1f);

            FlipHorizontally();
            StopAllCoroutines();
            StartCoroutine(KeepMoving());
        }

        private void FlipHorizontally()
        {
            _facingRight = !_facingRight;

            var theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private IEnumerator KeepMoving()
        {
            _anim.SetBool("Walking", true);

            bool hasSpace = Physics2D.OverlapCircle
                (GroundCheck.position, _groundCheckRadius, WhatIsGround);

            while (hasSpace)
            {
                _rigidbody2D.velocity = new Vector2(_facingRight ? MoveForce : -MoveForce, 0);

                yield return new WaitForSeconds(0.1f);

                hasSpace = Physics2D.OverlapCircle(GroundCheck.position, _groundCheckRadius, WhatIsGround);
            }

            StopAllCoroutines();
            StartCoroutine(IdleForARandomPeriod());
        }
    }
}