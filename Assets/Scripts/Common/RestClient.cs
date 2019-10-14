using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RestClient : MonoBehaviour
{
    private const string API_KEY = "DHNs5dw0xCZOJdgUsVGq";
    static RestClient instance;

    [SerializeField] GameObject loadingCircle;


    public static RestClient Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RestClient>();
                if (instance == null)
                {
                    GameObject restClientObject = new GameObject();
                    restClientObject.name = typeof(RestClient).Name;
                    instance = restClientObject.AddComponent<RestClient>();
                }
            }

            return instance;
        }
    }

    public IEnumerator Get(string uri, System.Action<bool, string, ScoreList> callBack = null)
    {
        //callBack Action 's bool stands for isError which checking if request is completed or not
        //string is for error message
        using (UnityWebRequest www = UnityWebRequest.Get(uri))
        {
            www.SetRequestHeader("APIKey", API_KEY);
            SetActiveLoadingCirlcle(true);
            yield return www.SendWebRequest();
            SetActiveLoadingCirlcle(false);
            if (www.isNetworkError)
            {
                if (callBack != null)
                {
                    callBack(true, www.error, null);
                    Debug.LogError(www.error);
                }
            }
            else if (www.isHttpError)
            {
                if (callBack != null)
                {
                    Debug.LogError("Error code: " + www.responseCode + "\nMaybe there is something wrong with Game ID");
                    callBack(true, "Error code: " + www.responseCode, null);
                }
            }
            else if (www.isDone)
            {
                if (www.responseCode == 200)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    jsonResult = JsonHelper.FixArrayJson(jsonResult, JsonHelper.GetMemberName((ScoreList sc) => sc.Items));
                    ScoreList scoreList = JsonUtility.FromJson<ScoreList>(jsonResult);
                    if (callBack != null)
                    {
                        callBack(false, "", scoreList);
                    }
                }
                else
                {
                    if (callBack != null)
                    {
                        callBack(true, www.downloadHandler.text, null);
                    }
                }
            }
        }
    }

    public IEnumerator Post(string uri, Score score, System.Action<bool, string, ScoreList> callBack = null)
    {
        //Same as Get method, in callback action, bool is for isError, string is for error message
        if (!GameStates.Instance.isSendingPostRequest)
        {
            string jsonData = JsonUtility.ToJson(score);
            using (UnityWebRequest www = UnityWebRequest.Post(uri, jsonData))
            {
                www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
                www.uploadHandler.contentType = "application/json";
                www.SetRequestHeader("APIKey", API_KEY);
                SetActiveLoadingCirlcle(true);
                GameStates.Instance.isSendingPostRequest = true;

                yield return www.SendWebRequest();

                GameStates.Instance.isSendingPostRequest = false;
                SetActiveLoadingCirlcle(false);
                if (www.isNetworkError)
                {
                    Debug.LogError(www.error);
                    if (callBack != null)
                    {
                        Debug.LogError(www.error);
                        callBack(true, www.error, null);
                    }
                }
                else if (www.isHttpError)
                {
                    if (callBack != null)
                    {
                        Debug.LogError("Error code: " + www.responseCode);
                        callBack(true, "Error code: " + www.responseCode, null);
                    }
                }
                else if (www.isDone)
                {
                    if (callBack != null)
                    {
                        string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                        jsonResult = JsonHelper.FixArrayJson(jsonResult, JsonHelper.GetMemberName((ScoreList sc) => sc.Items));
                        callBack(false, "", JsonUtility.FromJson<ScoreList>(jsonResult));
                    }
                }
                else
                {
                    if (callBack != null)
                    {
                        Debug.LogError(www.downloadHandler.text);
                        callBack(true, www.downloadHandler.text, null);
                    }
                }
            }
        }
    }
    void SetActiveLoadingCirlcle(bool value)
    {
        if (loadingCircle != null)
        {
            loadingCircle.SetActive(value);
        }
    }
}
[System.Serializable]
public class Score
{
    public string PlayerName;
    public int Value;
    public int GameID;
}


[System.Serializable]
public class ScoreList
{
    public List<Score> Items;
}

