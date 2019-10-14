using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGainArea : MonoBehaviour {
    float colliderHeight;
    bool isActivated = false;

    public void SetSize(float height)
    {
        isActivated = false;
        GetComponent<BoxCollider2D>().size = new Vector2(GameStats.Instance.DefaultScoreAreaWidth, height);
    }
    public void SetSize(float width,float height)
    {
        isActivated = false;
        GetComponent<BoxCollider2D>().size = new Vector2(width, height);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated || !GameStates.Instance.isAlive) { return; }
        GameStats.Instance.AddPlayerScore();
        isActivated = true;
        if (GameStats.Instance.ScoreSFX)
        {
            AudioSource.PlayClipAtPoint(GameStats.Instance.ScoreSFX, Camera.main.transform.position, GameStats.Instance.ScoreSFXVolume);
        }
    }
}
