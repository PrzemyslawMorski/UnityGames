using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameLoop : MonoBehaviour
    {
        public GameObject MemoButtonsPanel;
        public Text ScoreText;
        private static int _score;

        private const int MatchReward = 10;
        private const int MissPenalty = -2;

        public static int NumMemoButtons = 16;

        private MemoButton _lastClickedMemoButton;
        private bool _memoButtonsClickingAllowed;

        void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            StopAllCoroutines();

            ResetScore();
            StartCoroutine(ResetMemoButtons());
        }

        public void ClickedMemoButton(MemoButton memoButton)
        {
            if (_memoButtonsClickingAllowed) StartCoroutine(ClickedMemoButtonCoroutine(memoButton));
        }

        private void ChangeScore(int scoreChange)
        {
            _score += scoreChange;

            if (_score < 0) _score = 0;

            ScoreText.text = "SCORE: " + _score;
        }

        private IEnumerator ClickedMemoButtonCoroutine(MemoButton memoButton)
        {
            if (!memoButton.ShowingBack) yield break;
            memoButton.ShowFront();

            if (_lastClickedMemoButton == null)
            {
                _lastClickedMemoButton = memoButton;
                yield break;
            }

            _memoButtonsClickingAllowed = false;
            yield return new WaitForSeconds(1);
            if (MemoButton.MemoMatch(memoButton, _lastClickedMemoButton))
            {
                ChangeScore(MatchReward);

                Destroy(_lastClickedMemoButton.gameObject);
                Destroy(memoButton.gameObject);

                _lastClickedMemoButton = null;
                _memoButtonsClickingAllowed = true;
            }
            else
            {
                ChangeScore(MissPenalty);
                if (_lastClickedMemoButton != null) _lastClickedMemoButton.ShowBack();
                memoButton.ShowBack();
                _lastClickedMemoButton = null;

                _memoButtonsClickingAllowed = true;
            }
        }

        private void ResetScore()
        {
            _score = 0;
            ScoreText.text = "SCORE: 0";
        }

        private IEnumerator ResetMemoButtons()
        {
            _lastClickedMemoButton = null;
            _memoButtonsClickingAllowed = true;

            var memoButtons = GameObject.FindGameObjectsWithTag("MemoButton");
            yield return null;
            foreach (var memoButton in memoButtons)
            {
                Destroy(memoButton);
                yield return null;
            }

            MemoButtonsPanel.GetComponent<MemoButtons>().ShowMemoButtons();
        }
    }
}