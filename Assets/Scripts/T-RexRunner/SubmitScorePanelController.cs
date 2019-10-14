using Assets.Scripts.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitScorePanelController : MonoBehaviour {
    Text playerScoreNumber;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if (playerScoreNumber!=null)
        {
            return;
        }
        else if(RexMovement.IsWaiting && playerScoreNumber==null)
        {
            playerScoreNumber = CanvasGUIHelpers.GetTextByName(transform, "PlayerScoreNumber");
            playerScoreNumber.text = Math.Floor(GUIMenu.PersonalScore).ToString().PadLeft(5, '0');
            return;
        }
    }
}
