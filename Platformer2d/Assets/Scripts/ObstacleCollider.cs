using UnityEngine;

namespace Assets.Scripts
{
    public class ObstacleCollider : MonoBehaviour {

        public int DamageDone = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.tag.Contains("Player")) return;

            Debug.Log("Obstacle trigger2d Enter");

            other.gameObject.GetComponent<PlayerController>().Hit(DamageDone);

            other.attachedRigidbody.AddForce(new Vector2(-20 * other.attachedRigidbody.velocity.x, -20 * other.attachedRigidbody.velocity.y));
        }
    }
}
