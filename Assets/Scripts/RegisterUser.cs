using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegisterUser : MonoBehaviour
{

    [SerializeField]
    TMP_InputField email, pass;

    public void SendPlayerInfo()
    {
        SaveData.RegisterUser(email.text, pass.text);
        email.text = "";
        pass.text = "";
    }

    public void SignInUser()
    {
        SaveData.SignInUser(email.text, pass.text);
        email.text = "";
        pass.text = "";
    }

    public void TestData()
    {
        string bajs = "Selmas bajs";
        SaveData.SendTestData(bajs);
    }

}
