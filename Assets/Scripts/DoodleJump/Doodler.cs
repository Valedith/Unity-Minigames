using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump
{
    public class Doodler : MonoBehaviour
    {
        Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                transform.position = touchPosition;
            }
        }
    }
}

