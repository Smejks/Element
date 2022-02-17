using Firebase.Auth;
using System.Threading.Tasks;
using SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Login
{
    public static async Task<FirebaseUser> RegisterUser(string email, string pass, string screenName)
    {
        Debug.Log("Attempting registration...");
        return await FBDatabase.auth.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWith(task => {
            if (task.Exception != null) {
                Debug.LogWarning(task.Exception);
                return null;
            }
            UserData newUser = new UserData(screenName);
            SaveManager.SaveObject($"users/{task.Result.UserId}", newUser);
            return task.Result;
        });
    }

    public static async Task<FirebaseUser> SignInUser(string email, string pass)
    {
        Debug.Log("Attempting sign in...");
        return await FBDatabase.auth.SignInWithEmailAndPasswordAsync(email, pass).ContinueWith(task => {
            if (task.Exception != null) {
                Debug.LogWarning(task.Exception);
                return null;
            }
            return task.Result;
        });
    }

    public static async Task<FirebaseUser> SignInAnonymous()
    {
        Debug.Log("Attempting anonymous sign in...");
        return await FBDatabase.auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.Exception != null) {
                Debug.LogWarning(task.Exception);
                return null;
            }
            return task.Result;
        });
    }
}
