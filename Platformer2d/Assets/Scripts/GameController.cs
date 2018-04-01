using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public Text HealthText;
        public Text CoinsText;

        public string[] LevelScenesNames;

        private int _currentLevel;

        public Transform SpawnPoint;

        private static GameController _controller;

        private GameObject _restartGamePrompt;

        public static bool GamePaused = false;

        private void Awake()
        {
            if (_controller == null)
            {
                DontDestroyOnLoad(gameObject);
                _controller = this;
            }
            else if (_controller != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _restartGamePrompt = GameObject.FindGameObjectWithTag("RestartGamePrompt");
            _restartGamePrompt.gameObject.SetActive(false);

            _currentLevel = Array.IndexOf(LevelScenesNames, SceneManager.GetActiveScene().name);

            ResetGame();
        }

        public void PlayerDied()
        {
            GamePaused = true;
            _restartGamePrompt.gameObject.SetActive(true);
        }

        public void PlayerFell()
        {
            ResetLevel();
        }

        private static void ResetGame()
        {
            ResetPlayer();
        }

        private static void ResetLevel()
        {
            ResetPlayer();
        }

        private static void ResetPlayer()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) Destroy(player);

            var spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

            Instantiate(Resources.Load("Prefabs/Player"), spawnPoint.transform.position, Quaternion.identity);
        }

        public void UpdateHealthStatus(int health)
        {
            HealthText.text = "Health: " + health;
        }

        public void UpdateNumCoins(int numCoins)
        {
            CoinsText.text = "Coins: " + numCoins;
        }

        public void PlayerQuits()
        {
            Application.Quit();
        }

        public void PlayerWantsReplay()
        {
            ResetGame();
            GamePaused = false;
        }

        public void ProceedToNextLevel()
        {
            _currentLevel++;

            if (_currentLevel == LevelScenesNames.Length)
            {
                EndGame();
                return;
            }

            SceneManager.LoadScene(LevelScenesNames[_currentLevel]);
        }

        private void EndGame()
        {
            Debug.Log("Finished game");
        }
    }
}