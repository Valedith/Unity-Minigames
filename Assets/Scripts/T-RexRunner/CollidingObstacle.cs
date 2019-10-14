using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingObstacle : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="REX")
        {
            RexMovement.IsDead = true;
            RexMovement.IsStart = false;
        }
    }

}
