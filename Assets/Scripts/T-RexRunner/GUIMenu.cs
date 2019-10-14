using Assets.Scripts.Helpers;
using Assets.Scripts.StoredData;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIMenu : MonoBehaviour
{
    [SerializeField] Text firstRankedScore;
    [SerializeField] Text secondRankedScore;
    [SerializeField] Text thirdRankedScore;

    [SerializeField] Text firstRankedName;
    [SerializeField] Text secondRankedName;
    [SerializeField] Text thirdRankedName;

    [SerializeField] GameObject errorPanel;
    [SerializeField] Text errorText;

    Text highscoreText;
    Text highscoreNumber;
    Text scoreNumber;
    Text gameOverText;
    Image restartIcon;
    RectTransform leaderboardPanelRectTransform;
    RectTransform submitScorePanelRectTransform;

    static CanvasGroup leaderboardPanelCanvasGroup;
    static CanvasGroup submitScorePanelCanvasGroup;

    static Image leaderboardPanelImage;
    static Image submitScorePanelImage;

    double personalHighscore;
    static double personalScore;

    public static double PersonalScore
    {
        get
        {
            return personalScore;
        }

        set
        {
            personalScore = value;
        }
    }

    public static CanvasGroup LeaderboardPanelCanvasGroup
    {
        get
        {
            return leaderboardPanelCanvasGroup;
        }

        set
        {
            leaderboardPanelCanvasGroup = value;
        }
    }

    public static CanvasGroup SubmitScorePanelCanvasGroup
    {
        get
        {
            return submitScorePanelCanvasGroup;
        }

        set
        {
            submitScorePanelCanvasGroup = value;
        }
    }

    public static Image LeaderboardPanelImage
    {
        get
        {
            return leaderboardPanelImage;
        }

        set
        {
            leaderboardPanelImage = value;
        }
    }

    public static Image SubmitScorePanelImage
    {
        get
        {
            return submitScorePanelImage;
        }

        set
        {
            submitScorePanelImage = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        Init();
        HideAllComponents();
    }
    void Init()
    {
        highscoreText = CanvasGUIHelpers.GetTextByName(transform, "HighscoreText");
        highscoreNumber = CanvasGUIHelpers.GetTextByName(transform, "HighscoreNumber");
        scoreNumber = CanvasGUIHelpers.GetTextByName(transform, "ScoreNumber");

        leaderboardPanelRectTransform = CanvasGUIHelpers.GetRectTransformByName(transform, "LeaderboardPanel");
        submitScorePanelRectTransform = CanvasGUIHelpers.GetRectTransformByName(transform, "SubmitScorePanel");

        LeaderboardPanelCanvasGroup = leaderboardPanelRectTransform.GetComponent<CanvasGroup>();
        SubmitScorePanelCanvasGroup = submitScorePanelRectTransform.GetComponent<CanvasGroup>();

        LeaderboardPanelImage = leaderboardPanelRectTransform.GetComponent<Image>();
        SubmitScorePanelImage = submitScorePanelRectTransform.GetComponent<Image>();

        highscoreNumber.text = Math.Floor(StoredGUIProperties.StoredHighscore).ToString().PadLeft(5, '0');

        personalScore = 0;
        personalHighscore = StoredGUIProperties.StoredHighscore;
    }
    // Update is called once per frame
    void Update()
    {
        if (!RexMovement.IsStart && !RexMovement.IsDead || RexMovement.IsWaiting)
        {
            return;
        }
        else if (RexMovement.IsStart && !RexMovement.IsDead)
        {
            StartGamePopUp();
        }
        else if (RexMovement.IsDead && !RexMovement.IsStart)
        {
            RexMovement.IsWaiting = true;
            GameOverPopUp();
            if (StoredGUIProperties.StoredHighscore < personalHighscore)
            {
                StoredGUIProperties.StoredHighscore = personalHighscore;
            }
            //if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Mouse0))
            //{
            //    SceneManager.LoadScene("SampleScene");
            //}
            return;
        }
        CountScore();
    }
    void CountScore()
    {
        personalScore += 0.2;
        scoreNumber.text = Math.Floor(personalScore).ToString().PadLeft(5, '0');
        if (personalScore > personalHighscore)
        {
            personalHighscore = personalScore;
            highscoreNumber.text = Math.Floor(personalHighscore).ToString().PadLeft(5, '0');
        }
    }
    void HideAllComponents()
    {
        highscoreText.enabled = false;
        highscoreNumber.enabled = false;
        scoreNumber.enabled = false;

        CanvasGUIHelpers.SwitchVisibleCanvasGroup(LeaderboardPanelCanvasGroup, LeaderboardPanelImage, false, "Leaderboard");

        CanvasGUIHelpers.SwitchVisibleCanvasGroup(SubmitScorePanelCanvasGroup, SubmitScorePanelImage, false, "SubmitScore");
    }
    void StartGamePopUp()
    {
        scoreNumber.enabled = true;
    }
    void GameOverPopUp()
    {
        highscoreText.enabled = true;
        highscoreNumber.enabled = true;

        CanvasGUIHelpers.SwitchVisibleCanvasGroup(SubmitScorePanelCanvasGroup, SubmitScorePanelImage, true, "SubmitScore");
    }

    //OK
    public void SetUpLoseScreen()
    {
        StartCoroutine(RestClient.Instance.Get(ClientAPIs.Instance.UriWithGameID, SetUpLeaderboard));
    }

    //OK
    public void SetUpLeaderboard(bool isError, string errorMessage, ScoreList scoreList)
    {
        CheckErrorConnection(isError, errorMessage);
        if (isError) { return; }
        switch (scoreList.Items.Count)
        {
            case 0:
                {
                    SetRankedScore("0".PadLeft(5, '0'), "0".PadLeft(5, '0'), "0".PadLeft(5, '0'));
                    SetRankedName("NULL", "NULL", "NULL");
                    break;
                }
            case 1:
                {
                    SetRankedScore(scoreList.Items[0].Value.ToString().PadLeft(5, '0'), "0".PadLeft(5, '0'), "0".PadLeft(5, '0'));
                    SetRankedName(scoreList.Items[0].PlayerName, "NULL", "NULL");
                    break;
                }
            case 2:
                {
                    SetRankedScore(scoreList.Items[0].Value.ToString().PadLeft(5, '0'), scoreList.Items[1].Value.ToString().PadLeft(5, '0'), "0".PadLeft(5, '0'));
                    SetRankedName(scoreList.Items[0].PlayerName, scoreList.Items[1].PlayerName, "NULL");
                    break;
                }
            case 3:
                {
                    SetRankedScore(scoreList.Items[0].Value.ToString().PadLeft(5, '0'), scoreList.Items[1].Value.ToString().PadLeft(5, '0'), scoreList.Items[2].Value.ToString().PadLeft(5, '0'));
                    SetRankedName(scoreList.Items[0].PlayerName, scoreList.Items[1].PlayerName, scoreList.Items[2].PlayerName);
                    break;
                }
            default:
                {
                    SetRankedScore(scoreList.Items[0].Value.ToString().PadLeft(5, '0'), scoreList.Items[1].Value.ToString().PadLeft(5, '0'), scoreList.Items[2].Value.ToString().PadLeft(5, '0'));
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
    void SetRankedScore(string first, string second, string third)
    {
        firstRankedScore.text = first;
        secondRankedScore.text = second;
        thirdRankedScore.text = third;
    }
    //OK
    void SetRankedName(string first, string second, string third)
    {
        firstRankedName.text = first.ToString();
        secondRankedName.text = second.ToString();
        thirdRankedName.text = third.ToString();
    }
}
