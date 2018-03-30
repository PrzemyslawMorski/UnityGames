using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgain : MonoBehaviour
{
    public GameLoop Game;

    public void RestartGame()
    {
        Game.StartGame(Game.NumDisappearingButtons);
    }
}
