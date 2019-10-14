using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Helpers
{
    public class CanvasGUIHelpers
    {
        public static Text GetTextByName(Transform parent, string name)
        {
            Transform child = parent.Find(name);
            return child.GetComponent<Text>();
        }
        public static GameObject GetGameObjectByName(Transform parent, string name)
        {
            Transform child = parent.Find(name);
            return child.GetComponent<GameObject>();
        }
        public static RectTransform GetRectTransformByName(Transform parent, string name)
        {
            Transform child = parent.Find(name);
            return child.GetComponent<RectTransform>();
        }
        public static Image GetImageByName(Transform parent, string name)
        {
            Transform child = parent.Find(name);
            return child.GetComponent<Image>();
        }
        public static void SwitchVisibleCanvasGroup(CanvasGroup canvasGroup, Image image, bool toVisible, string types)
        {
            if(toVisible)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                image.raycastTarget = true;
                switch (types)
                {
                    case "SubmitScore":
                        canvasGroup.GetComponent<SubmitScorePanelController>().enabled = true;
                        break;
                    case "Leaderboard":
                        canvasGroup.GetComponent<LeaderboardPanelController>().enabled = true;
                        break;
                    default: break;
                }
            }
            else if(!toVisible)
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                image.raycastTarget = false;
                switch (types)
                {
                    case "SubmitScore":
                        canvasGroup.GetComponent<SubmitScorePanelController>().enabled = false;
                        break;
                    case "Leaderboard":
                        canvasGroup.GetComponent<LeaderboardPanelController>().enabled = false;
                        break;
                    default: break;
                }
            }
        }
    }
}
