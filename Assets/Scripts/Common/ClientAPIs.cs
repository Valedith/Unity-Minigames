using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientAPIs : MonoBehaviour {
    [Tooltip("Game ID can be seen on database")] public int gameID;
    [Header("API calls")]
    [SerializeField] string uri;

    static ClientAPIs instance;

    public static ClientAPIs Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ClientAPIs>();
                if (instance == null)
                {
                    GameObject newGameObject = new GameObject();
                    newGameObject.name = typeof(ClientAPIs).Name;
                    instance = newGameObject.AddComponent<ClientAPIs>();
                }
            }
            return instance;
        }
    }
    public string UriWithGameID
    {
        get
        {
            if (!uri[uri.Length - 1].Equals(char.Parse("/")))
            {
                uri += "/";
            }
            return Uri+gameID;
        }
    }

    public string Uri
    {
        get
        {
            return uri;
        }
    }
}
