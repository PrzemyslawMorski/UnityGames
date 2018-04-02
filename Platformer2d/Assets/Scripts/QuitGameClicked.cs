using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class QuitGameClicked : MonoBehaviour, IPointerClickHandler
    {
        private GameController _game;

        private void Start()
        {
            _game = FindObjectOfType<GameController>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _game.PlayerQuits();
            Destroy(GameObject.FindGameObjectWithTag("RestartGamePrompt"));
        }
    }
}