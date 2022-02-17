using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] TMP_InputField email, password, screenName;
    public void Register()
    {
        User.RegisterUser(email.text, password.text, screenName.text);
        email.text = "";
        password.text = "";
    }

    public void SignIn()
    {
        User.SignIn(email.text, password.text);
        email.text = "";
        password.text = "";
    }

    public void FindGame()
    {
        if (User.user == null)
            return;
        User.MatchMake();
    }
}
