using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameObjectHelpers
    {
        public static Vector2 ObjectMovingHorizontally(string objectTag, float speed)
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(objectTag);
            return gameObject.transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }
}
