using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    private static SceneController _instance;
    public static SceneController Instance { get { return _instance; } }

    int currentScene;

   

    void Start()
    {

    }

    void Update()
    {

    }

    public void NextScene()
    {
        SceneManager.LoadScene(1);
        
        //currentScene = SceneManager.GetActiveScene().buildIndex;
        //if (currentScene < SceneManager.sceneCount - 1)
        //    SceneManager.LoadScene(currentScene + 1);
        //else
        //    SceneManager.LoadScene(1);

    }
    public void PrevScene()
    {
        SceneManager.LoadScene(0);
    }

}
