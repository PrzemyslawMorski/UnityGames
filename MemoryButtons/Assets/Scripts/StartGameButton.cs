using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class StartGameButton : MonoBehaviour, IPointerClickHandler
    {
        private GameLoop _gameLoop;

        private bool _allowedResetingGame = true;

        void Start ()
        {
            _gameLoop = GameObject.FindWithTag("MainCamera").GetComponent<GameLoop>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_allowedResetingGame) return;

            StartCoroutine(InitStartGame());
        }

        private IEnumerator InitStartGame()
        {
            _allowedResetingGame = false;
            _gameLoop.StartGame();
            yield return new WaitForSeconds(1);
            _allowedResetingGame = true;
        }
    }
}
