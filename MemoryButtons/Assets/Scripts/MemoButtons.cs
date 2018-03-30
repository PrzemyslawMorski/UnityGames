using UnityEngine;

namespace Assets.Scripts
{
    public class MemoButtons : MonoBehaviour
    {

        public void ShowMemoButtons()
        {
            var memoButtons = MemoButtonFactory.BuildMemoButtons(GameLoop.NumMemoButtons, gameObject.GetComponent<RectTransform>());

            foreach (var memoButton in memoButtons)
            {
                memoButton.gameObject.SetActive(true);
            }
        }
    }
}
