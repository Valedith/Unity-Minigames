using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {
    [SerializeField] float scrollSpeed = 0.5f;
    [SerializeField] GameController gameController;
    Material myMaterial;
    Vector2 offSet;

	// Use this for initialization
	void Start () {
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(scrollSpeed, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (GameStates.Instance.isWaiting || !GameStates.Instance.isAlive) { return; }
        myMaterial.mainTextureOffset += offSet*Time.deltaTime;
	}
}
