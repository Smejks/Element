using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;


public class FirebaseDemo : MonoBehaviour {
    FirebaseDatabase db;

    void Start() {
        //Setup for talking to firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            //Log if we get any errors from the opperation
            if (task.Exception != null)
                Debug.LogError(task.Exception);

            //the database
            db = FirebaseDatabase.DefaultInstance;

            //Set the value World to the key Hello in the database
            db.RootReference.Child("Hello").SetValueAsync("World");
        });
    }
}