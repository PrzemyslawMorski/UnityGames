using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MemoButton : MonoBehaviour
    {
        private Sprite _frontImage;
        private Sprite _backImage;
        private GameLoop _gameLoop;

        public bool ShowingBack;

        void Start()
        {
            _gameLoop = GameObject.FindWithTag("MainCamera").GetComponent<GameLoop>();
        }

        public void SetFrontImage(Sprite front)
        {
            _frontImage = front;
        }

        public void SetBackImage(Sprite back)
        {
            _backImage = back;
        }

        public void ShowFront()
        {
            if (gameObject != null) gameObject.GetComponent<Image>().sprite = _frontImage;
            ShowingBack = false;
        }

        public void ShowBack()
        {
            if(gameObject != null) gameObject.GetComponent<Image>().sprite = _backImage;
            ShowingBack = true;
        }

        public void OnPointerClick()
        {
            _gameLoop.ClickedMemoButton(this);
        }

        public static bool MemoMatch(MemoButton lhs, MemoButton rhs)
        {
            return lhs != null && rhs != null &&
                lhs._frontImage == rhs._frontImage &&
                lhs._backImage != rhs._backImage;
        }
    }
}
