using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public string[] LevelScenesNames;
        private int _currentLevel;

        private static GameController _controller;
        private static GameObject _player;

        public static bool GamePaused;

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
            _currentLevel = Array.IndexOf(LevelScenesNames, SceneManager.GetActiveScene().name);
            ResetGame();
        }

        public void PlayerDied()
        {
            GamePaused = true;
            LaunchRestartGamePrompt();
        }

        //RestartGamePrompt calls PlayerQuits or PlayerWantsReplay methods
        private static void LaunchRestartGamePrompt()
        {
            if (GameObject.FindGameObjectWithTag("RestartGamePrompt") != null) return;

            var foreground = GameObject.Find("Foreground");
            Instantiate(Resources.Load("Prefabs/RestartGamePrompt"), foreground.transform, false);
        }

        public void PlayerFell()
        {
            ResetLevel();
        }

        private void ResetGame()
        {
            var levelName = LevelScenesNames[0];
            _currentLevel = 0;

            RespawnPlayer();
            ResetPlayerStats();
            UpdateLevelName(levelName);
        }

        private static void ResetPlayerStats()
        {
            var playerController = _player.GetComponent<PlayerController>();
            playerController.HealToFull();
            playerController.ResetGold();
        }

        private void ResetLevel()
        {
            RespawnPlayer();
        }

        private void RespawnPlayer()
        {
            var spawnPoint = GameObject.Find("SpawnPoint");

            if (_player == null)
            {
                _player = Instantiate(Resources.Load("Prefabs/Player"), spawnPoint.transform.position,
                    Quaternion.identity) as GameObject;

                DontDestroyOnLoad(_player);
            }
            _player.GetComponent<PlayerController>().Game = this;
            _player.transform.position = spawnPoint.transform.position;
            _player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        public void UpdateHealthStatus(int health)
        {
            var healthText = GameObject.Find("HealthText").GetComponent<Text>();
            if (healthText != null) healthText.text = "Health: " + health;
        }

        public void UpdateNumCoins(int numCoins)
        {
            var coinsText = GameObject.Find("GoldText").GetComponent<Text>();
            if (coinsText != null) coinsText.text = "Coins: " + numCoins;
        }

        private static void UpdateLevelName(string levelName)
        {
            var levelNameText = GameObject.Find("LevelNameText").GetComponent<Text>();
            if (levelNameText != null) levelNameText.text = levelName;
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

            if (_currentLevel >= LevelScenesNames.Length)
            {
                EndGame();
            }
            else
            {
                GamePaused = true;

                var levelName = LevelScenesNames[_currentLevel];
                SceneManager.LoadScene(levelName);

                RespawnPlayer();
                UpdateLevelName(levelName);

                GamePaused = false;
            }
        }

        private static void EndGame()
        {
            Debug.Log("Finished game");
        }
    }
}