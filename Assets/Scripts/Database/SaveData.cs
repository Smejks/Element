using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public struct UserData
    {
        public string screenName;
        public int score;

        public UserData(string screenName = "unnamed")
        {
            this.screenName = screenName;
            score = 0;
        }
        public override string ToString() => $"player: (screenName: {screenName}, score: {score})";
    }

    [Serializable]
    public class GameData
    {
        public string gameID;
        public bool gameIsActive;
        public PlayerGameData[] players;

        public GameData(string gameID)
        {
            this.gameID = gameID;
            gameIsActive = false;
            players = new PlayerGameData[2];
        }
        public override string ToString() => $"game: (game ID: {gameID})";
    }
    [Serializable]
    public struct PlayerGameData
    {
        public string screenName;
        public List<string> sequence;
        public bool ready;

        public PlayerGameData(string screenName = "unassigned")
        {
            this.screenName = screenName;
            sequence = new List<string>();
            ready = false;
        }

    }



}