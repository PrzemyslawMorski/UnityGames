using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject GamePanel;
    public GameObject EndScreenPanel;

    private float _scoredTimeInSeconds;

    public int NumDisappearingButtons = 20;

    void Start()
    {
        ShowMainMenu();
    }

    void Update()
    {
        if (GamePanel.activeSelf)
        {
            _scoredTimeInSeconds += Time.deltaTime;
        }
    }

    private IEnumerator CheckGameEndCondition()
    {
        var numDisappearingButtons = GamePanel.transform.Find("ButtonsPanel").childCount;

        while (numDisappearingButtons != 0)
        {
            numDisappearingButtons = GamePanel.transform.Find("ButtonsPanel").childCount;

            UpdateRemainingButtonsMessage(numDisappearingButtons);

            yield return new WaitForSeconds(0.1f);
        }

        EndGame();
    }

    private void UpdateRemainingButtonsMessage(int numVisibleDisappearingButtons)
    {
        var remainingButtonsMessage = 
            GamePanel.transform.Find("RemainingButtonsMessage").gameObject.GetComponent<Text>();

        remainingButtonsMessage.text = "Remaining buttons: " + numVisibleDisappearingButtons;
    }

    private void EndGame()
    {
        HideGamePanel();
        ShowEndScreenPanel();
    }

    private void ShowEndScreenPanel()
    {
        var scoredTimeText = EndScreenPanel.transform.Find("ScoredTime").GetComponent<Text>();

        var scoredTimeRounded = Math.Round(_scoredTimeInSeconds, 2);

        scoredTimeText.text = "You clicked " + NumDisappearingButtons + " buttons in " + scoredTimeRounded + " seconds.";

        EndScreenPanel.SetActive(true);
    }

    private void HideGamePanel()
    {
        GamePanel.SetActive(false);
    }

    private void ShowDisappearingButtonsOnPanel(Transform panel)
    {
        for (var i = 0; i < NumDisappearingButtons; i++)
        {
            var newButton =
                DisappearingButtonFactory.BuildDisappearingButtonForRectContainer(panel.GetComponent<RectTransform>()
                    .rect);
            newButton.transform.SetParent(panel, false);
        }
    }

    private void HideMainMenu()
    {
        MainMenuPanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        _scoredTimeInSeconds = 0f;

        MainMenuPanel.SetActive(true);
        GamePanel.SetActive(false);
        EndScreenPanel.SetActive(false);
    }

    public void StartGame(int numDisappearingButtons)
    {
        NumDisappearingButtons = numDisappearingButtons;
        _scoredTimeInSeconds = 0f;

        HideMainMenu();
        HideEndScreen();

        ShowGamePanel();
        StartCoroutine(CheckGameEndCondition());
    }
    
    private void HideEndScreen()
    {
        EndScreenPanel.SetActive(false);   
    }

    private void ShowGamePanel()
    {
        var buttonsPanel = GamePanel.transform.Find("ButtonsPanel");

        ShowDisappearingButtonsOnPanel(buttonsPanel);

        GamePanel.SetActive(true);
    }
}