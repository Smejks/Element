using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerSaveData
{
    public string email;
    public string pass;
    public PlayerSaveData(string email, string pass)
    {
        this.email = email;
        this.pass = pass;
    }


}

public class SequenceData : MonoBehaviour
{
    public List<string> sequence;



    public SequenceData(List<string> sequence)
    {
        this.sequence = sequence;
    }


}
