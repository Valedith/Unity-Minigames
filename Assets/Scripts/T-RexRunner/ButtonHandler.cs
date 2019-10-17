using Assets.Scripts.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour {
    [SerializeField] GUIMenu guiMenu;
    [SerializeField] Text nameInputField;
    public void RestartScene()
    {
        SceneManager.LoadScene("TRexRunner");
    }
    public void CancelSubmit()
    {
        CanvasGUIHelpers.SwitchVisibleCanvasGroup(GUIMenu.SubmitScorePanelCanvasGroup, GUIMenu.SubmitScorePanelImage, false, "SubmitScore");

        CanvasGUIHelpers.SwitchVisibleCanvasGroup(GUIMenu.LeaderboardPanelCanvasGroup, GUIMenu.LeaderboardPanelImage, true, "Leaderboard");

        guiMenu.SetUpLoseScreen();
    }
    public void ExitGame()
    {

    }
    public void SubmitScore()
    {
        if (string.IsNullOrEmpty(nameInputField.text.Trim()))
        {
            guiMenu.ShowNameInputFieldError("Name can't be null");
            return;
        }
        Score newScore = new Score { PlayerName = nameInputField.text.Trim(), Value = (int) Math.Floor(GUIMenu.PersonalScore), GameID = ClientAPIs.Instance.gameID };
        StartCoroutine(RestClient.Instance.Post(ClientAPIs.Instance.Uri, newScore, guiMenu.HandleSubmitScore));
    }
    public void ShowSubmitScore()
    {
        guiMenu.ShowSubmitWindow();
    }
}
