using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

public class SaveData : MonoBehaviour
{
    private static SaveData _instance;
    public static SaveData Instance { get { return _instance; } }

    static FirebaseAuth auth;
    static string currentUser = null;

    public List<string> localSequence = new List<string>();
    public List<string> remoteSequence = new List<string>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogError(task.Exception);
            auth = FirebaseAuth.DefaultInstance;
        });
    }

    public static void RegisterUser(string email, string pass)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError(task.Exception);
                return;
            }
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
        });
    }

    public static void SignInUser(string email, string pass)
    {
        auth.SignInWithEmailAndPasswordAsync(email, pass).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError(task.Exception);
                return;
            }
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            currentUser = newUser.UserId;
        });
    }

    public static void SendTestData(string data)
    {
        if (currentUser == null)
            return;
        var db = FirebaseDatabase.DefaultInstance;
        var task = db.RootReference.Child("users").Child(currentUser).Child("TestData").SetValueAsync(data).ContinueWith(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);
            else Debug.Log("Inserted data: " + data);
        });
    }


    static void SaveToFirebase(string data)
    {
        var db = FirebaseDatabase.DefaultInstance;
    }


}
