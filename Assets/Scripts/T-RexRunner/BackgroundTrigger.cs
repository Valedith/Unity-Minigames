using Assets.Scripts;
using Assets.Scripts.Constants;
using UnityEngine;

public class BackgroundTrigger : MonoBehaviour {
    float triggerMovementSpeed;
    string currentBackground;

    Vector2 originalTriggerPosition;
    Vector2 backgroundTailPosition;
    
    Vector2 background1CurrentPosition;
    Vector2 background2CurrentPosition;


    GameObject objectBackground1;
    GameObject objectBackground2;
	
    // Use this for initialization
	void Start () {
        Init();
        GetAllOriginalPositions();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!RexMovement.IsStart||RexMovement.IsDead)
        {
            return;
        }
        MovingTrigger();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "VANISHINGTRIGGER")
        {
            ShiftBackground();
            RevertTriggerPosition();
        }
    }
    void Init()
    {
        currentBackground = "BACKGROUND1";
        objectBackground1 = GameObject.FindWithTag("BACKGROUND1");
        objectBackground2 = GameObject.FindWithTag("BACKGROUND2");
        triggerMovementSpeed = NonPlayerMovementConstants.NONPLAYER_MOVEMENT;
    }
    void GetAllOriginalPositions()
    {
        originalTriggerPosition = transform.position;
        backgroundTailPosition = objectBackground2.transform.position;
    }
    void RevertTriggerPosition()
    {
        transform.position = originalTriggerPosition;
    }
    void ShiftBackground()
    {
        if (currentBackground.Equals("BACKGROUND1"))
        {
            objectBackground1.transform.position = backgroundTailPosition;
            currentBackground = "BACKGROUND2";
        }
        else if (currentBackground.Equals("BACKGROUND2"))
        {
            objectBackground2.transform.position = backgroundTailPosition;
            currentBackground = "BACKGROUND1";
        }
    }

    void MovingTrigger()
    {
        transform.position = GameObjectHelpers.ObjectMovingHorizontally(tag, triggerMovementSpeed);
    }

}
