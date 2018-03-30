using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public GameLoop Game;

    public void NavigateToMainMenu()
    {
        Game.ShowMainMenu();
    }
}
