using UnityEngine;

namespace Assets.Scripts
{
    public class CoinCollider : MonoBehaviour
    {
        public int NumCoins = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.tag.Contains("Player")) return;
            
            other.gameObject.GetComponent<PlayerController>().PickUpCoins(NumCoins);
            Destroy(gameObject);
        }
    }
}
