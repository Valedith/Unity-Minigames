using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour {
    [SerializeField] GUIMenu guiMenu;
    public void RestartScene()
    {
        SceneManager.LoadScene("SampleScene");
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
    public void SubmtScore()
    {

    }
}
