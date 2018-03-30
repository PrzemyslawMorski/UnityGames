using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public GameLoop Game;

    public NumButtonsErrorMessage InputErrorText;

    public void OnPlayClicked(InputField numButtonsToShowInput)
    {
        var numButtonsToShow = ExtractNumButtons(numButtonsToShowInput);

        if (numButtonsToShow < 1 || numButtonsToShow > 30)
        {
            InputErrorText.ShowRangeError();
            return;
        }

        Game.StartGame(numButtonsToShow);
    }

    private static int ExtractNumButtons(InputField input)
    {
        int numButtonsToShow;

        var parseSucceeded = int.TryParse(input.text, out numButtonsToShow);

        if (!parseSucceeded)
        {
            return -1;
        }

        return numButtonsToShow;
    }

}
