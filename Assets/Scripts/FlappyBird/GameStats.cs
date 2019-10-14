using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStats : MonoBehaviour{
    [Header("Pipe stats")]
    [SerializeField] float pipeMovementSpeed = 5f;
    [SerializeField]
    [Tooltip("Width of scoreArea Collider if it's not set")]
    float defaultScoreAreaWidth = 0.5f;
    
    [Header("SFX")]
    [SerializeField] AudioClip scoreSFX;
    [SerializeField] [Range(0f,1f)] float scoreSFXVolume = 1f;

    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0f, 1f)] float deathSFXVolume = 1f;

    [SerializeField] AudioClip flapSFX;
    [SerializeField] [Range(0f, 1f)] float flapSFXVolume = 1f;
 
    [Header("Game Stats")]
    [SerializeField] int playerScore = 0;
    [SerializeField] int scoreForEachPass = 10;

    [Header("References")]
    [SerializeField] CanvasesManager canvasesManager;
    static GameStats instance;



    public float PipeMovementSpeed
    {
        get
        {
            return pipeMovementSpeed;
        }
    }

    public static GameStats Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameStats>();
                if(instance == null)
                {
                    GameObject gameStats = new GameObject("GameStats");
                    gameStats.AddComponent<GameStats>();
                    instance = gameStats.GetComponent<GameStats>();
                }
            }
            return instance;
        }
    }

    public float DefaultScoreAreaWidth
    {
        get
        {
            return defaultScoreAreaWidth;
        }
    }

    public AudioClip ScoreSFX
    {
        get
        {
            return scoreSFX;
        }
    }

    public float ScoreSFXVolume
    {
        get
        {
            return scoreSFXVolume;
        }
    }

    public AudioClip DeathSFX
    {
        get
        {
            return deathSFX;
        }
    }

    public float DeathSFXVolume
    {
        get
        {
            return deathSFXVolume;
        }

        set
        {
            deathSFXVolume = value;
        }
    }

    public AudioClip FlapSFX
    {
        get
        {
            return flapSFX;
        }
    }

    public float FlapSFXVolume
    {
        get
        {
            return flapSFXVolume;
        }

        set
        {
            flapSFXVolume = value;
        }
    }

    public int PlayerScore
    {
        get
        {
            return playerScore;
        }
    }

    public void AddPlayerScore(int amount)
    {
        playerScore += amount;
        canvasesManager.UpdateScoreText(playerScore);
    }

    public void AddPlayerScore()
    {
        playerScore += scoreForEachPass;
        canvasesManager.UpdateScoreText(playerScore);
    }

}
