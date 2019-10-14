using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] GameObject pipeHeadPrefab;
    [SerializeField] GameObject pipeBodyPrefab;
    GameObject pipeHead, pipeBody;
    Camera mainCamera;
    BoxCollider2D pipeCollider;
    private void Awake()
    {
        pipeHead = Instantiate(pipeHeadPrefab, Vector2.zero, Quaternion.identity);
        pipeBody = Instantiate(pipeBodyPrefab, Vector2.zero, Quaternion.identity);
        pipeHead.transform.parent = transform;
        pipeBody.transform.parent = transform;

        mainCamera = Camera.main;
        pipeCollider = GetComponent<BoxCollider2D>();
        if (!pipeCollider)
        {
            pipeCollider = gameObject.AddComponent<BoxCollider2D>();
        }
        pipeCollider.isTrigger = true;
        pipeCollider.offset = Vector2.zero;
    }

    public void Place(float xPosition, float height, bool isAtBottom)
    {
        float cameraEdge = mainCamera.orthographicSize;
        cameraEdge = isAtBottom ? cameraEdge * -1 : cameraEdge;
        float pipeHeadHalfSizeY = (pipeHead.GetComponent<SpriteRenderer>().bounds.size / 2).y;
        var pipeBodySpriteRenderer = pipeBody.GetComponent<SpriteRenderer>();

        //Set position of Pipe GameObject to the middle of Head +Body ( for positioning collider )
        if (isAtBottom)
        {
            transform.position = new Vector3(xPosition, cameraEdge + height/2);
        }
        else
        {
            transform.position = new Vector3(xPosition, cameraEdge - height/2);
        }

        //Set position to Pipe Head
        if (isAtBottom)
        {
            pipeHead.transform.position = new Vector2(xPosition, cameraEdge + height - pipeHeadHalfSizeY);
        }
        else
        {
            pipeHead.transform.position = new Vector2(xPosition, cameraEdge - height + pipeHeadHalfSizeY);
        }

        //Set position to Pipe Body
        pipeBody.transform.position = new Vector2(xPosition, cameraEdge);
        

        if (isAtBottom)
        {
            pipeBodySpriteRenderer.size = new Vector2(pipeBodySpriteRenderer.size.x, height - pipeHeadHalfSizeY * 2);
        }
        else
        {
            pipeBodySpriteRenderer.size = new Vector2(pipeBodySpriteRenderer.size.x, -height + pipeHeadHalfSizeY * 2);
        }
        
        pipeCollider.size = new Vector2(pipeBody.GetComponent<SpriteRenderer>().bounds.size.x, height);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BirdController>())
        {
            collision.GetComponent<BirdController>().Die();
        }
    }
}
