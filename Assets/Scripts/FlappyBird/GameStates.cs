using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public bool isWaiting = true;
    public bool isAlive = true;
    public bool isSendingPostRequest = false;

    static GameStates instance;

    public static GameStates Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameStates>();
            }
            if(instance == null)
            {
                GameObject gameStates = new GameObject("GameStates");
                instance = gameStates.AddComponent<GameStates>();
            }
            return instance;
        }
    }
}
