using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float MaxSpeed = 10f;
        public float JumpForce = 700f;
        public int MaxHealth = 3;

        private bool _facingRight = true;
        private Animator _anim;
        private Rigidbody2D _rigidbody2D;
        private GameController _game;

        private bool _grounded = false;
        private const float GroundRadius = 0.2f;

        public Transform GroundCheck;
        public LayerMask WhatIsGround;

        private int _numCoins;
        private int _health;

        private void Start()
        {
            _numCoins = 0;

            _health = MaxHealth;
            _anim = GetComponent<Animator>();
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

            _game = FindObjectOfType<GameController>();

            _game.UpdateHealthStatus(_health);
            _game.UpdateNumCoins(_numCoins);
        }

        private void FixedUpdate()
        {
            if (GameController.GamePaused)
            {
                _rigidbody2D.velocity = new Vector2(0f, 0f);
                _anim.SetBool("Ground", true);
                _anim.SetFloat("vSpeed", 0f);
                _anim.SetFloat("Speed", 0f);
                return;
            }

            if (!GetComponent<SpriteRenderer>().isVisible)
            {
                _game.PlayerFell();
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

        public void PickUpCoins(int numCoins)
        {
            if (numCoins < 0) return;

            _numCoins += numCoins;
            _game.UpdateNumCoins(_numCoins);
        }

        public void Hit(int healthLost)
        {
            if (healthLost < 0) return;

            _health -= healthLost;

            if (_health <= 0)
            {
                _game.PlayerDied();
            }
            else
            {
                _game.UpdateHealthStatus(_health);
            }
        }

        public void Heal(int healthHealed)
        {
            if (healthHealed <= 0) return;

            _health += healthHealed;
            _game.UpdateHealthStatus(_health);
        }
    }
}