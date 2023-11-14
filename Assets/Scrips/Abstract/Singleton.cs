using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    string goName = typeof(T).ToString();
                    GameObject go = GameObject.Find(goName);
                    if (go == null)
                    {
                        go = new GameObject();
                        go.name = goName;
                    }
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void OnApplicationQuit()
    {
        _instance = null;
    }
}
