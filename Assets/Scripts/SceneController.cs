using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Update()
    {
    }

    public void Confirm()
    {
        StartCoroutine(CombatResolutionScene());
    }

    public IEnumerator CombatResolutionScene()
    {
        audioController.PlaySFX(10);
        audioController.PlaySFX(11);
        yield return new WaitForSeconds(0.5f);
        audioController.PlaySFX(13);
        yield return new WaitForSeconds(0.5f);
        if (FBDatabase.db.RootReference.Child($"games/{User.activeGame.gameID}/0") != null && FBDatabase.db.RootReference.Child($"games/{User.activeGame.gameID}/1") != null)
            SceneManager.LoadScene(2);
    }

    public async void LoadArena()
    {
        await User.MatchMake();
        if (User.playerIndex == 0)
            FBDatabase.db.GetReference($"games/{User.activeGame.gameID}/activeGame").ValueChanged += AttemptMatchStart;
        else
            AttemptMatchStart();
    }

    public void AttemptMatchStart(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null) {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        if (currentScene != 0) {
            audioController.PlaySFX(12);
            SceneManager.LoadScene(1);
        }
    }

    public void AttemptMatchStart()
    {
        SceneManager.LoadScene(1);
    }

}