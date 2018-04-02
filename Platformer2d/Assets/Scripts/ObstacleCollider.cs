using UnityEngine;

namespace Assets.Scripts
{
    public class ObstacleCollider : MonoBehaviour {

        public int DamageDone = 1;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.tag.Contains("Player")) return;

            other.gameObject.GetComponent<PlayerController>().Hit(DamageDone);

            other.rigidbody.velocity = Vector2.zero;

            other.rigidbody.AddForce(new Vector2(-20 * other.relativeVelocity.x, -20 * other.relativeVelocity.y));
        }
    }
}
