using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumButtonsErrorMessage : MonoBehaviour {

    public void ShowRangeError()
    {
        var textComponent = gameObject.GetComponent<Text>();

        if(textComponent == null) return;

        textComponent.text = "Please type in a number larger than 0 and smaller than 30.";
    }

    public void HideError()
    {
        var textComponent = gameObject.GetComponent<Text>();

        if (textComponent == null) return;

        textComponent.text = "";
    }
}
