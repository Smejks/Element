using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBDatabase : MonoBehaviour
{
    private static FBDatabase _instance;
    public static FBDatabase Instance { get { return _instance; } }
    public static FirebaseAuth auth;
    public static FirebaseDatabase db;

    void Awake()
    {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(this.gameObject);
        }

        db = FirebaseDatabase.DefaultInstance;

    }

    void Start()
    {
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                if (task.Exception != null) { Debug.LogError(task.Exception.ToString()); return; }
                auth = FirebaseAuth.DefaultInstance;
            });
        }


    }
}
