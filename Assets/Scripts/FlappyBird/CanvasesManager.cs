using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasesManager : MonoBehaviour
{
    [Header("Score Canvas")]
    [SerializeField] Canvas scoreCanvas;
    [SerializeField] Text scoreText;
    [SerializeField] Text tapToPlayText;

    [Header("Lose Canvas")]
    [SerializeField] Canvas loseCanvas;
    [SerializeField] Text playerScoreValueText;

    [SerializeField] Text firstRankedScore;
    [SerializeField] Text secondRankedScore;
    [SerializeField] Text thirdRankedScore;

    [SerializeField] Text firstRankedName;
    [SerializeField] Text secondRankedName;
    [SerializeField] Text thirdRankedName;

    [SerializeField] GameObject errorPanel;
    [SerializeField] Text errorText;

    [SerializeField] GameObject showSubmitPanelButton;
    [SerializeField] GameObject submitPanel;
    [SerializeField] InputField playerNameInput;
    [SerializeField] Text playerNameErrorText;

    public void UpdateScoreText(int amount)
    {
        scoreText.text = amount.ToString();
    }

    public void TurnOffTapToPlayText()
    {
        tapToPlayText.gameObject.SetActive(false);
    }

    public void SetActiveLoseCanvas(bool value)
    {
        playerScoreValueText.text = GameStats.Instance.PlayerScore.ToString();
        if(value)
        {
            SetUpLoseScreen();
            submitPanel.SetActive(false);
        }
        loseCanvas.gameObject.SetActive(value);
    }

    public void SetActiveScoreCanvas(bool value)
    {
        scoreCanvas.gameObject.SetActive(value);
    }

    //OK
    public void SetUpLoseScreen()
    {
        StartCoroutine(RestClient.Instance.Get(ClientAPIs.Instance.UriWithGameID, SetUpLeaderboard));
    }

    //OK
    public void SetUpLeaderboard(bool isError, string errorMessage,ScoreList scoreList)
    {
        CheckErrorConnection(isError, errorMessage);
        if (isError) { return; }
        switch (scoreList.Items.Count)
        {
            case 0:
                {
                    SetRankedScore(0, 0, 0);
                    SetRankedName("", "", "");
                    break;
                }
            case 1:
                {
                    SetRankedScore(scoreList.Items[0].Value, 0, 0);
                    SetRankedName(scoreList.Items[0].PlayerName, "", "");
                    break;
                }
            case 2:
                {
                    SetRankedScore(scoreList.Items[0].Value, scoreList.Items[1].Value, 0);
                    SetRankedName(scoreList.Items[0].PlayerName, scoreList.Items[1].PlayerName, "");
                    break;
                }
            case 3:
                {
                    SetRankedScore(scoreList.Items[0].Value, scoreList.Items[1].Value, scoreList.Items[2].Value);
                    SetRankedName(scoreList.Items[0].PlayerName, scoreList.Items[1].PlayerName, scoreList.Items[2].PlayerName);
                    break;
                }
            default:
                {
                    SetRankedScore(scoreList.Items[0].Value, scoreList.Items[1].Value, scoreList.Items[2].Value);
                    SetRankedName(scoreList.Items[0].PlayerName, scoreList.Items[1].PlayerName, scoreList.Items[2].PlayerName);
                    break;
                }
        }
    }
    //OK
    void CheckErrorConnection(bool isError, string errorMessage)
    {
        if (isError)
        {
            errorText.text = errorMessage;
            errorPanel.SetActive(true);
        }
        else
            errorPanel.SetActive(false);
    }
    //OK
    void SetRankedScore(int first, int second, int third)
    {
        firstRankedScore.text = first.ToString();
        secondRankedScore.text = second.ToString();
        thirdRankedScore.text = third.ToString();
    }
    //OK
    void SetRankedName(string first, string second, string third)
    {
        firstRankedName.text = first.ToString();
        secondRankedName.text = second.ToString();
        thirdRankedName.text = third.ToString();
    }

    //OK
    public void ShowSubmitWindow()
    {
        playerNameErrorText.gameObject.SetActive(false);
        submitPanel.SetActive(true);
    }

    //OK
    public void ShowNameInputFieldError(string error)
    {
        playerNameErrorText.text = error;
        playerNameErrorText.gameObject.SetActive(true);
    }

    //OK
    public void HandleSubmitScore(bool isError, string errorMessage, ScoreList scoreList)
    {
        if (!isError)
        {
            submitPanel.SetActive(false);
            showSubmitPanelButton.SetActive(false);
        }
        SetUpLeaderboard(isError, errorMessage, scoreList);
    }
}
