using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveData;

public class SceneController : MonoBehaviour
{
    private static SceneController _instance;
    public static SceneController Instance { get { return _instance; } }

    OpponentController opponentController;
    AudioController audioController;
    int currentScene;


    void Start()
    {
        audioController = FindObjectOfType<AudioController>();
        opponentController = FindObjectOfType<OpponentController>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void Confirm()
    {
        StartCoroutine(PlayConfirmationSounds());
        if (User.playerIndex == 0)
            FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/players/1/ready").ValueChanged += AttemptResolutionStart;
        else
            FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/players/0/ready").ValueChanged += AttemptResolutionStart;
    }

    public IEnumerator PlayConfirmationSounds()
    {
        audioController.PlaySFX(10);
        audioController.PlaySFX(11);
        yield return new WaitForSeconds(0.5f);
        audioController.PlaySFX(13);
    }

    public void AttemptResolutionStart(object sender, ValueChangedEventArgs args)
    {
        Debug.Log(args.Snapshot.Value);
        if (!(bool)args.Snapshot.Value) { return; }

        if (User.activeGame.players[User.playerIndex].ready) {
            if (User.playerIndex == 0) {
                FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/players/1/ready").ValueChanged -= AttemptResolutionStart;
            }
            else {
                FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/players/0/ready").ValueChanged -= AttemptResolutionStart;
            }
            Debug.Log("Other player readied up! Resolving combat!");
            SceneManager.LoadScene(2);
        }
    }




    public async void LoadArena()
    {
        await User.MatchMake();
        if (User.playerIndex == 0)
            FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/gameIsActive").ValueChanged += AttemptMatchStart;
        else
            AttemptMatchStart();
    }

    public void AttemptMatchStart(object sender, ValueChangedEventArgs args)
    {
        Debug.Log(args.Snapshot.Value);
        if (!(bool)args.Snapshot.Value) { return; }

        if (currentScene != 0) {
            audioController.PlaySFX(12);
        }
        FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/gameIsActive").ValueChanged -= AttemptMatchStart;
        audioController.audiosource.Stop();
        Debug.Log("Player joined! Starting match...");
        SceneManager.LoadScene(1);
    }


    public void AttemptMatchStart()
    {
        audioController.audiosource.Stop();
        Debug.Log("Game Found! Starting match...");
        SceneManager.LoadScene(1);
    }


    public void Rematch()
    {
        ResetPlayersForRematch();
    }

    public async void ResetPlayersForRematch()
    {
        User.activeGame.players[User.playerIndex].ready = false;
        await SaveManager.SaveObject($"games/{User.activeGame.gameID}/players/{User.playerIndex}/", User.activeGame.players[User.playerIndex]);

        if (User.playerIndex == 0) {
            PlayerGameData opponentData = await SaveManager.LoadObject<PlayerGameData>($"games/{User.activeGame.gameID}/players/1/");
            if (!opponentData.ready) {
                AttemptRematchStart();
            }
            else {
                FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/players/1/ready").ValueChanged += AttemptRematchStart;
            }
        }
        else {
            PlayerGameData opponentData = await SaveManager.LoadObject<PlayerGameData>($"games/{User.activeGame.gameID}/players/0/");
            if (!opponentData.ready) {
                AttemptRematchStart();
            }
            else {
                FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/players/1/ready").ValueChanged += AttemptRematchStart;
            }
        }
    }

    public void AttemptRematchStart(object sender, ValueChangedEventArgs args)
    {
        if (User.playerIndex == 0) {
            FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/players/1/ready").ValueChanged -= AttemptRematchStart;
        }
        else {
            FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/players/0/ready").ValueChanged -= AttemptRematchStart;
        }
        Debug.Log("Other player readied up! Initializing Rematch!");
        SceneManager.LoadScene(1);
    }


    public void AttemptRematchStart()
    {
        Debug.Log("Other player already waiting! Initializing Rematch!");
        SceneManager.LoadScene(1);
    }

    void OnDestroy()
    {
        FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/gameIsActive").ValueChanged -= AttemptMatchStart;
        if (User.playerIndex == 0) {
            FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/players/1/ready").ValueChanged -= AttemptResolutionStart;
            FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/players/1/ready").ValueChanged -= AttemptRematchStart;
        }
        else {
            FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/players/0/ready").ValueChanged -= AttemptResolutionStart;
            FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/players/0/ready").ValueChanged -= AttemptRematchStart;
        }

    }

}