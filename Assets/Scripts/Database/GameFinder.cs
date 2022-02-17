using SaveData;
using System.Threading.Tasks;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameFinder
{
    public static async Task<GameData> FindGame()
    {
        List<GameData> games = await SaveManager.LoadMultipleObjects<GameData>("games/");
        if (games == null) {
            Debug.Log("No games found!");
            return null;
        }
        foreach (GameData game in games) {
            if (!game.activeGame) {
                Debug.Log("Open game found!");
                game.players[1] = new PlayerGameData(User.data.screenName);
                game.activeGame = true;
                Debug.Log("Joined open game!");
                if (!await SaveManager.SaveObject($"games/{game.gameID}", game))
                    return null;
                return game;
            }
        }
        Debug.Log("No open games found!");
        return null;
    }

    public static async Task<GameData> CreateGame()
    {
        Debug.Log("Creating Game...");
        string key = FBDatabase.db.RootReference.Child("games/").Push().Key;
        GameData game = new GameData(key);
        game.players[0] = new PlayerGameData(User.data.screenName);
        if (!await SaveManager.SaveObject($"games/{game.gameID}", game)) {
            return null;
        }
        return game;
    }

    public static async Task<GameData> LeaveGame()
    {
        Debug.Log("Leaving Game...");
        GameData currentGame = User.activeGame;
        //foreach (var player in currentGame.players) {
        //    if (player.screenName == User.data.screenName) {
        //        User.activeGame = null;
                if (!await SaveManager.RemoveNode<bool>($"games/{currentGame.gameID}")) {
                    return null;
                }
            
        return null;

    }



}
