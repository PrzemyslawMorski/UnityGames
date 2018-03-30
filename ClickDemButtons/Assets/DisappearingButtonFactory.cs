using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class DisappearingButtonFactory
{
    private static readonly Button DisappearingButtonPrefab = Resources.Load("Button", typeof(Button)) as Button;

    //button width constraints
    private const float MaxButtonWidthComparedToContainerWidth = 0.2f;
    private const float MinButtonWidthComparedToContainerWidth = 0.1f;

    //button height constraints
    private const float MaxButtonHeightComparedToContainerHeight = 0.2f;
    private const float MinButtonHeightComparedToContainerHeight = 0.1f;

    public static Button BuildDisappearingButtonForRectContainer(Rect container)
    {
        var newButton = Object.Instantiate(DisappearingButtonPrefab);

        newButton.SetColor();
        newButton.SetSize(container);
        newButton.SetPosition(container);
        newButton.onClick.AddListener(DestroyClickedButton);

        return newButton;
    }

    public static void DestroyClickedButton()
    {
        var clickedButton = EventSystem.current.currentSelectedGameObject;
        Object.Destroy(clickedButton);
    }

    private static void SetPosition(this Button button, Rect container)
    {
        var buttonTransform = button.GetComponent<RectTransform>().rect;

        var minMoveX = buttonTransform.width;
        var minMoveY = buttonTransform.height;

        var maxMoveX = container.width - buttonTransform.width;
        var maxMoveY = container.height - buttonTransform.height;

        var moveX = Random.Range(minMoveX, maxMoveX);
        var moveY = Random.Range(minMoveY, maxMoveY);

        var newPosition = button.transform.position;
        newPosition.x += moveX;
        newPosition.y += moveY;

        button.transform.position = newPosition;
    }

    private static void SetColor(this Button button)
    {
        button.GetComponent<Image>().color = Random.ColorHSV();
    }

    private static void SetSize(this Button button, Rect container)
    {
        var buttonTransform = button.GetComponent<RectTransform>();

        var newWidth =
            Random.Range(MinButtonWidthComparedToContainerWidth * container.width,
            MaxButtonWidthComparedToContainerWidth * container.width);

        var newHeight =
            Random.Range(MinButtonHeightComparedToContainerHeight * container.height,
                MaxButtonHeightComparedToContainerHeight * container.height);

        buttonTransform.sizeDelta = new Vector2(newWidth, newHeight);
    }
}
