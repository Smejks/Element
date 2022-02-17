using Firebase.Auth;
using SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    private static User _instance;
    public static User Instance { get { return _instance; } }

    public static UserData data;
    public static FirebaseUser user;
    public static GameData activeGame = null;
    public static string userPath;

    void Awake()
    {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(this.gameObject);
        }

    }

    #region login/register user
    public async static void RegisterUser(string email, string pass, string screenName)
    {
        if (user != null) {
            print("User is already logged in");
            return;
        }
        user = await Login.RegisterUser(email, pass, screenName);
        LoadUserData();
    }

    public async static void SignIn(string email, string pass)
    {
        if (user != null) {
            print("User is already logged in");
            return;
        }
        user = await Login.SignInUser(email, pass);
        LoadUserData();
    }

    public async static void AnonymousSignIn()
    {
        if (user != null) {
            print("User is already logged in");
            return;
        }
        user = await Login.SignInAnonymous();
    }

    #endregion

    #region matchmaking

    public static async void MatchMake()
    {
        GameData foundGame;
        foundGame = await GameFinder.FindGame();
        if (foundGame != null) {
            activeGame = foundGame;
            print(activeGame.ToString());
            return;
        }
        foundGame = await GameFinder.CreateGame();
        if (foundGame != null) {
            activeGame = foundGame;
            return;
        }
        print("Something went wrong whilst matchmaking");
    }

    #endregion

    static async void LoadUserData()
    {
        userPath = $"users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}";
        data = await SaveManager.LoadObject<UserData>(userPath);
        print(data.ToString());
    }


}