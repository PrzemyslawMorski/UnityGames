using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class RestartGameClicked : MonoBehaviour, IPointerClickHandler
    {
        private GameController _game;

        private void Start()
        {
            _game = FindObjectOfType<GameController>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _game.PlayerWantsReplay();
            Destroy(GameObject.FindGameObjectWithTag("RestartGamePrompt"));
        }
    }
}