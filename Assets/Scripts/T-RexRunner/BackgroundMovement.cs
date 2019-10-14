using Assets.Scripts;
using Assets.Scripts.Constants;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour {
    float backgroundMovingSpeed;
    // Use this for initialization
    void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        if (!RexMovement.IsStart||RexMovement.IsDead)
        {
            return;
        }
        BackgroundMoving();
    }
    void Init()
    {
        backgroundMovingSpeed = NonPlayerMovementConstants.NONPLAYER_MOVEMENT;
    }
    void BackgroundMoving()
    {
        transform.position = GameObjectHelpers.ObjectMovingHorizontally(tag, backgroundMovingSpeed);
    }
}
