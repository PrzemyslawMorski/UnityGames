using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Assets.Scripts
{
    public static class MemoButtonFactory
    {
        private static readonly Button MemoButtonPrefab =
            Resources.Load("Prefabs/MemoButton", typeof(Button)) as Button;

        public static IEnumerable<Button> BuildMemoButtons(int numMemoButtons, RectTransform container)
        {
            if (numMemoButtons % 2 != 0) numMemoButtons--;
            if (numMemoButtons <= 0) return new Button[] { };

            var memoButtons = new Button[numMemoButtons];

            var oneRowOfButtons = (int) Math.Ceiling(Math.Sqrt(numMemoButtons));

            var frontImageIndex = 1;
            for (var i = 0; i < numMemoButtons; i += 2)
            {
                var frontImage = LoadImage(frontImageIndex++);
                memoButtons[i] = BuildMemoButton(container, frontImage);
                memoButtons[i + 1] = BuildMemoButton(container, frontImage);
            }

            {
                // shuffle Memo Buttons
                var rnd = new System.Random();
                memoButtons = memoButtons.OrderBy(x => rnd.Next()).ToArray();
            }

            var backImages = BuildBackImages(oneRowOfButtons);

            for (var i = 0; i < numMemoButtons; i++)
            {
                var memoButton = memoButtons[i];

                memoButton.SetSize(container, oneRowOfButtons);
                memoButton.SetPosition(oneRowOfButtons, i);

                var memoButtonScript = memoButton.GetComponent<MemoButton>();

                memoButtonScript.SetBackImage(backImages[i]);
                memoButtonScript.ShowBack();
            }

            return memoButtons;
        }

        private static Sprite[] BuildBackImages(int oneSideLength)
        {
            var backImageTexture = Resources.Load<Sprite>("Sprites/MemoButtons/back_image");

            return backImageTexture == null ? null : CutIntoPieces(backImageTexture, oneSideLength);
        }

        private static Sprite[] CutIntoPieces(Sprite sprite, int oneSideLength)
        {
            var cellWidth = sprite.texture.width / oneSideLength;
            var cellHeight = sprite.texture.height / oneSideLength;

            var pieces = new Sprite[oneSideLength * oneSideLength];

            var nextIndex = 0;

            for (var i = 0; i < oneSideLength; i++)
            {
                for (var j = 0; j < oneSideLength; j++)
                {
                    var cellRect = new Rect(i * cellWidth, j * cellHeight, cellWidth, cellHeight);
                    var cellSprite =
                        Sprite.Create(sprite.texture, cellRect, new Vector2(0, 0), sprite.pixelsPerUnit);
                    pieces[nextIndex++] = cellSprite;
                }
            }

            return pieces;
        }

        private static Button BuildMemoButton(RectTransform parent, Sprite front)
        {
            var memoButton = Object.Instantiate(MemoButtonPrefab);
            memoButton.gameObject.SetActive(false);
            memoButton.transform.SetParent(parent);
            memoButton.GetComponentInChildren<Text>().text = "";

            var memoButtonScript = memoButton.GetComponent<MemoButton>();
            memoButtonScript.SetFrontImage(front);

            return memoButton;
        }

        private static Sprite LoadImage(int nth)
        {
            return Resources.Load<Sprite>("Sprites/MemoButtons/pic" + nth);
        }

        private static void SetSize(this Button button, RectTransform container, int buttonsOnOneSide)
        {
            var buttonHeight = (container.rect.height / buttonsOnOneSide) * (90f / 100f);

            var rect = button.transform as RectTransform;
            if (rect != null)
            {
                rect.sizeDelta = new Vector2(buttonHeight, buttonHeight);
            }
        }

        private static void SetPosition(this Button memoButton, int buttonsOnOneSide, int index)
        {
            var parentTransform = memoButton.transform.parent.GetComponent<RectTransform>();
            memoButton.transform.position = memoButton.transform.parent.position;

            var buttonCellHeight = (parentTransform.rect.height / buttonsOnOneSide);
            var buttonCellWidth = (parentTransform.rect.width / buttonsOnOneSide);

            memoButton.transform.Translate(new Vector3(
                parentTransform.rect.xMin + buttonCellWidth / 2,
                parentTransform.rect.yMin + buttonCellHeight / 2,
                0)
            );

            var xIndex = index / buttonsOnOneSide;
            var yIndex = index % buttonsOnOneSide;

            var xOffset = buttonCellWidth * xIndex;
            var yOffset = buttonCellHeight * yIndex;

            memoButton.transform.Translate(new Vector3(xOffset, yOffset, 0));
        }
    }
}