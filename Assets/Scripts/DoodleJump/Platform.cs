using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] float jumpForce = 5f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Rigidbody2D targetRB = collision.collider.GetComponent<Rigidbody2D>();
            if(targetRB != null)
            {
                Vector2 newVelo = targetRB.velocity;
                newVelo.y = jumpForce;
                targetRB.velocity = newVelo;
            }
        }

    }
}

