using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerSaveData
{
    public string nick;
    public List<string> sequence;
    public int score;
    public PlayerSaveData(string nick, List<string> sequence, int score)
    {
        this.nick = nick;
        this.sequence = sequence;
        this.score = score;
    }
}
