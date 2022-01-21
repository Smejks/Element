using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Serializable]
    public struct PlayerData
    {
        public string name;
        public int score;
    }

}