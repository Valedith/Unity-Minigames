using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairOfPipes : MonoBehaviour {
    [SerializeField] GameObject PipePrefab;
    [SerializeField] GameObject scoreGainAreaPrefab;
    Pipe topPipe, bottomPipe;
    ScoreGainArea scoreGainArea;
    float movementSpeed;
    Camera mainCamera;

    private void Awake()
    {
        topPipe = Instantiate(PipePrefab).GetComponent<Pipe>();
        bottomPipe = Instantiate(PipePrefab).GetComponent<Pipe>();
        scoreGainArea = Instantiate(scoreGainAreaPrefab).GetComponent<ScoreGainArea>();
        topPipe.transform.parent = transform;
        bottomPipe.transform.parent = transform;
        scoreGainArea.transform.parent = transform;
        mainCamera = Camera.main;

        GameObject pipes = GameObject.Find("Pipes");
        if (!pipes)
        {
            pipes = new GameObject("Pipes");
        }
        transform.parent = pipes.transform;
    }
    // Use this for initialization
    void Start()
    {
        movementSpeed = GameStats.Instance.PipeMovementSpeed;
    }

    public void Place(float xPosition, float gapCenterPositionY, float gapHeight)
    {
        float mainCameraSize = mainCamera.orthographicSize;
        float bottomPipeHeight = mainCameraSize + gapCenterPositionY - (gapHeight - (gapHeight / 2));

        transform.position = new Vector2(xPosition, 0);

        bottomPipe.Place(xPosition, bottomPipeHeight, true);

        topPipe.Place(xPosition, mainCameraSize * 2 - (bottomPipeHeight + gapHeight), false);
        
        scoreGainArea.SetSize(gapHeight);
        scoreGainArea.transform.position = new Vector2(xPosition, gapCenterPositionY);
    }

    private void Update()
    {
        if (!GameStates.Instance.isAlive || GameStates.Instance.isWaiting) { return; }
        Move();
    }

    public void Move()
    {
        transform.position += Vector3.left * movementSpeed * Time.deltaTime;
    }
}
