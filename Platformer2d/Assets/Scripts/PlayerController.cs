using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float MaxSpeed = 10f;
        public float JumpForce = 700f;
        public int MaxHealth = 3;
        public GameController Game;
        public Transform GroundCheck;
        public LayerMask WhatIsGround;

        private bool _facingRight = true;
        private Animator _anim;
        private Rigidbody2D _rigidbody2D;

        private bool _grounded = false;
        private const float GroundRadius = 0.2f;

        public int Health { get; private set; }
        public int Gold { get; private set; }

        private void Start()
        {
            _anim = GetComponent<Animator>();
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            Game = FindObjectOfType<GameController>();

            Gold = 0;
            Health = MaxHealth;

            Game.UpdateHealthStatus(Health);
            Game.UpdateNumCoins(Gold);
        }

        private void FixedUpdate()
        {
            if (GameController.GamePaused)
            {
                StopMovement();
                return;
            }

            var mainCamera = Camera.main;
            if (_rigidbody2D.position.y < -1 * mainCamera.orthographicSize)
            {
                Game.PlayerFell();
                return;
            }

            _grounded = Physics2D.OverlapCircle
                (GroundCheck.position, GroundRadius, WhatIsGround);

            _anim.SetBool("Ground", _grounded);
            _anim.SetFloat("vSpeed", _rigidbody2D.velocity.y);

            var move = Input.GetAxis("Horizontal");
            _anim.SetFloat("Speed", Mathf.Abs(move));

            _rigidbody2D.velocity = new Vector2(move * MaxSpeed, _rigidbody2D.velocity.y);

            var movingRight = move > 0;
            var movingLeft = move < 0;

            if ((movingRight && !_facingRight) || (movingLeft && _facingRight)) FlipHorizontally();
        }

        private void Update()
        {
            if (GameController.GamePaused) return;

            if (!_grounded || !Input.GetKeyDown(KeyCode.Space)) return;
            _anim.SetBool("Ground", false);
            _rigidbody2D.AddForce(new Vector2(0, JumpForce));
        }

        private void FlipHorizontally()
        {
            _facingRight = !_facingRight;
            var theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private void StopMovement()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _anim.SetBool("Ground", true);
            _anim.SetFloat("vSpeed", 0f);
            _anim.SetFloat("Speed", 0f);
        }

        public void PickUpCoins(int numCoins)
        {
            if (numCoins < 0) return;

            Gold += numCoins;
            Game.UpdateNumCoins(Gold);
        }

        public void Hit(int healthLost)
        {
            if (healthLost < 0) return;

            Health -= healthLost;

            if (Health <= 0)
            {
                Health = 0;
                Game.UpdateHealthStatus(Health);
                Game.PlayerDied();
            }
            else
            {
                Game.UpdateHealthStatus(Health);
            }
        }

        public void Heal(int healthHealed)
        {
            if (healthHealed <= 0) return;

            Health += healthHealed;

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            Game.UpdateHealthStatus(Health);
        }

        public void HealToFull()
        {
            Health = MaxHealth;
            Game.UpdateHealthStatus(Health);
        }

        public void ResetGold()
        {
            Gold = 0;
            Game.UpdateNumCoins(Gold);
        }
    }
}