using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp {
	public class PlayerController : MonoBehaviour {

		public float maxSpeed = 10f;
		public float jumpForce = 700f;

		bool facingRight = true;
		Animator anim;
		Rigidbody2D rigidbody2d;

		bool grounded = false;
		const float groundRadius = 0.2f;

		public Transform groundCheck;
		public LayerMask whatIsGround;

		void Start () {
			anim = GetComponent<Animator> ();
			rigidbody2d = gameObject.GetComponent<Rigidbody2D> ();
		}

		void FixedUpdate () {
			grounded = Physics2D.OverlapCircle 
				(groundCheck.position, groundRadius, whatIsGround);

			anim.SetBool ("Ground", grounded);
			anim.SetFloat ("vSpeed", rigidbody2d.velocity.y);

			float move = Input.GetAxis ("Horizontal");
			anim.SetFloat ("Speed", Mathf.Abs (move));

			rigidbody2d.velocity = new Vector2 (move * maxSpeed, rigidbody2d.velocity.y);

			bool movingRight = move > 0;
			bool movingLeft = move < 0;

			if ((movingRight && !facingRight) || (movingLeft && facingRight)) FlipHorizontally ();
		}


		void Update() {
			if(grounded && Input.GetKeyDown(KeyCode.Space)) {
				anim.SetBool ("Ground", false);
				rigidbody2d.AddForce(new Vector2(0, jumpForce));
			}
		}

		void FlipHorizontally() {
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
}

