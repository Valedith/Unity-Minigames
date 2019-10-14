using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Pipe")]
    [SerializeField] GameObject pipePrefab;
    [SerializeField] GameObject pipeHeadPrefab;
    [SerializeField] GameObject pipeBodyPrefab;
    [SerializeField] ScoreGainArea scoreGainAreaPrefab;
    [SerializeField] PairOfPipes pairOfPipesPrefab;

    [Header("Level")]
    [SerializeField] float distanceBetweenPipe = 5f;
    [SerializeField] float firstSpawPositionX;
    [SerializeField] float gapHeightMin = 2f;
    [SerializeField] float gapHeightMax = 5f;

    [Header("References")]
    [SerializeField] CanvasesManager canvasesManager;
    [SerializeField] InputField nameInputField;
    
    Camera mainCamera;

    GameObject lastSpawnPairOfPipes;

    // Use this for initialization
    private void Awake()
    {
        if (Application.isMobilePlatform)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
        mainCamera = Camera.main;
        GameStates.Instance.isWaiting = true;
    }
    void Start()
    {
        //pipeList = new List<PairOfPipes>();
        //firstSpawPositionX = mainCamera.ViewportToWorldPoint(Vector3.right).x + distanceBetweenPipe;
        canvasesManager.UpdateScoreText(GameStats.Instance.PlayerScore);
        canvasesManager.SetActiveLoseCanvas(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStates.Instance.isWaiting)
        {
            WaitForStartingGame();
            return;
        }
        if (!GameStates.Instance.isAlive) { return; }
        GenerateLevel();
    }

    void WaitForStartingGame()
    {
        if (Input.GetButtonUp("Fly"))
        {
            GameStates.Instance.isWaiting = false;
            FindObjectOfType<BirdController>().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            canvasesManager.TurnOffTapToPlayText();
        }
    }


    void CreatePairOfPipes(float xPosition, float gapCenterPositionY, float gapHeight)
    {
        GameObject pairOfPipes = ObjectPooler.Instance.GetPooledObject();
        if(pairOfPipes != null)
        {
            lastSpawnPairOfPipes = pairOfPipes;
            pairOfPipes.SetActive(true);
            pairOfPipes.GetComponent<PairOfPipes>().Place(xPosition, gapCenterPositionY, gapHeight);
        }
    }


    void GenerateLevel()
    {
        if (ObjectPooler.Instance.GetPooledObject() != null)
        {
            float gapHeight = Random.Range(gapHeightMin, gapHeightMax);
            float gapPositionY = Random.Range(-mainCamera.orthographicSize + gapHeight / 2, mainCamera.orthographicSize - gapHeight / 2);
            if (lastSpawnPairOfPipes == null)
            {
                CreatePairOfPipes(firstSpawPositionX, gapPositionY, gapHeight);
            }
            else
            {
                CreatePairOfPipes(lastSpawnPairOfPipes.transform.position.x + distanceBetweenPipe,
                                    gapPositionY, gapHeight);
            }
        }
    }



    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        canvasesManager.SetActiveLoseCanvas(true);
        canvasesManager.SetActiveScoreCanvas(false);
    }

    public void SubmitScore()
    {
        if (string.IsNullOrEmpty(nameInputField.text.Trim()))
        {
            canvasesManager.ShowNameInputFieldError("Name can't be null");
            return;
        }
        Score newScore = new Score { PlayerName = nameInputField.text.Trim(), Value = GameStats.Instance.PlayerScore, GameID = ClientAPIs.Instance.gameID };
        StartCoroutine(RestClient.Instance.Post(ClientAPIs.Instance.Uri, newScore, canvasesManager.HandleSubmitScore));
    }

    public void ShowSubmitWindow()
    {
        canvasesManager.ShowSubmitWindow();
    }

    public void BackToLoseWindow()
    {
        canvasesManager.SetActiveLoseCanvas(true);
    }
}
