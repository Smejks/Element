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
        if (opponentController.sequence != null)
        SceneManager.LoadScene(2);
    }

    public void LoadArena()
    {
        StartCoroutine(GetOpponentName());
    }

    public IEnumerator GetOpponentName()
    {
        yield return new WaitForSeconds(2);
        print(User.activeGame.players[0].screenName);
        if (User.activeGame.players[0].screenName != null && currentScene != 0)
            audioController.PlaySFX(12);
        SceneManager.LoadScene(1);
    }

}
