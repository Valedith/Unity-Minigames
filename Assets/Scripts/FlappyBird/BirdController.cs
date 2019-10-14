using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] float flyForce;
    [SerializeField] GameController gameController;

    Camera mainCamera;
    Rigidbody2D rb2D;


    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStates.Instance.isWaiting)
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionY;
            return;
        }
        Fly();
        Tilt();
    }

    void Fly()
    {
        if (!GameStates.Instance.isAlive) { return; }
        if (Input.GetButtonDown("Fly"))
        {
            rb2D.velocity = Vector2.up * flyForce;
            if (GameStats.Instance.FlapSFX)
            {
                AudioSource.PlayClipAtPoint(GameStats.Instance.FlapSFX, mainCamera.transform.position, GameStats.Instance.FlapSFXVolume);
            }
        }
    }

    void Tilt()
    {
        transform.eulerAngles = new Vector3(0f, 0f, Mathf.Clamp(rb2D.velocity.y * 5f, -90f, 30f));
    }

    public void Die()
    {
        if (!GameStates.Instance.isAlive) { return; }
        StartCoroutine(DeathCoroutine());
        
    }

    IEnumerator DeathCoroutine()
    {
        GameStates.Instance.isAlive = false;
        if (GameStats.Instance.DeathSFX)
        {
            AudioSource.PlayClipAtPoint(GameStats.Instance.DeathSFX, Camera.main.transform.position, GameStats.Instance.DeathSFXVolume);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        gameController.GameOver();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
