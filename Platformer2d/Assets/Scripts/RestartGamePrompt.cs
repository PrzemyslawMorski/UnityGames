using UnityEngine;

namespace Assets.Scripts
{
    public class RestartGamePrompt : MonoBehaviour
    {
        private GameController _game;

        private void Start()
        {
            _game = GameObject.FindObjectOfType<GameController>();
        }

        public void Reset()
        {
            _game.PlayerWantsReplay();
            gameObject.SetActive(false);
        }

        public void Quit()
        {
            _game.PlayerQuits();
            gameObject.SetActive(false);
        }
    }
}