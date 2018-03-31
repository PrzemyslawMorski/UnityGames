using UnityEngine;

namespace Assets.Scripts
{
    public class HealthCollider : MonoBehaviour
    {
        public int HealthHealed = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.tag.Contains("Player")) return;

            other.gameObject.GetComponent<PlayerController>().Heal(HealthHealed);
            Destroy(gameObject);
        }
    }
}
