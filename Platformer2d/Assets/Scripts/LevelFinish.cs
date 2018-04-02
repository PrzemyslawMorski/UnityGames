using UnityEngine;

namespace Assets.Scripts
{
    public class LevelFinish : MonoBehaviour
    {

        private GameController _game;

        void Start()
        {
            _game = FindObjectOfType<GameController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.tag.Contains("Player")) return;
            _game.ProceedToNextLevel();
        }
    }
}
