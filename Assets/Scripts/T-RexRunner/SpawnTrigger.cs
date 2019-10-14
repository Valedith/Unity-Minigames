using Assets.Scripts.Constants;
using Assets.Scripts.StoredData;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour {
    [SerializeField] List<GameObject> obstacleSamples;
    float cactusMovementSpeed;
    float currentPositionX;
    float spawningRate;
    float minDistanceValue;
    float maxDistanceValue;
    
    List<GameObject> obstacleObjects;
	// Use this for initialization
	void Start ()
    {
        Init();
    }
    void Update()
    {
        if (!RexMovement.IsStart || RexMovement.IsDead)
        {
            return;
        }
        CactusSpawning();
        CactusMoving();
    }
    private void Init()
    {
        StoredLevelProperties.Difficult = 0;
        obstacleObjects = new List<GameObject>();

        cactusMovementSpeed = NonPlayerMovementConstants.NONPLAYER_MOVEMENT;

        spawningRate = ObstaclePropertyConstants.BEGINNING_SPAWNING_RATE;
        minDistanceValue = ObstaclePropertyConstants.MIN_DISTANCE;
        maxDistanceValue = ObstaclePropertyConstants.MAX_DISTANCE;
        currentPositionX = ObstaclePropertyConstants.FIRST_SPAWNING_LOCATION_X;

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "CACTUS")
        {
            CactusDestroy(collision);
        }
    }
    void CactusDestroy(Collider2D collision)
    {
        obstacleObjects.RemoveAt(0);
        Destroy(collision.gameObject);
    }
    void CactusSpawning()
    {

        int obstacleObjectsCount = obstacleObjects.Count;
        
        if (obstacleObjectsCount < spawningRate)
        {

            GameObject randomObject = obstacleSamples[Random.Range(0, obstacleSamples.Count-1)];
            
            obstacleObjects.Add(Instantiate(randomObject, new Vector2(currentPositionX, randomObject.transform.position.y), Quaternion.identity));

            currentPositionX = obstacleObjects[obstacleObjectsCount].transform.position.x + Random.Range(minDistanceValue, maxDistanceValue);
        }
        StoredLevelProperties.Difficult += (System.Math.Round(GUIMenu.PersonalScore, 1) % 100 == 0) ? 1 : 0;
        spawningRate = StoredLevelProperties.Difficult + ObstaclePropertyConstants.BEGINNING_SPAWNING_RATE;
    }
    void CactusMoving()
    {
        foreach (GameObject obstacle in obstacleObjects)
        {
            obstacle.transform.position += new Vector3(cactusMovementSpeed * Time.deltaTime,0,0);
        }
    }
}
