using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController _instance;
    public static SceneController Instance { get { return _instance; } }

    AudioController audioController;
    int currentScene;
   

    void Start()
    {
        audioController = FindObjectOfType<AudioController>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
    }

    public void Confirm()
    {
        StartCoroutine("NextScene");
    }

    public IEnumerator NextScene()
    {
        audioController.PlaySFX(10);
        audioController.PlaySFX(11);
            yield return new WaitForSeconds(0.5f);
        audioController.PlaySFX(13);
            yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
    }

    public void FightScene()
    {
        if (currentScene != 0)
        audioController.PlaySFX(12);
        SceneManager.LoadScene(1);
    }



}
