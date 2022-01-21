using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    private static SceneController _instance;
    public static SceneController Instance { get { return _instance; } }

    int currentScene;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void ChangeScene()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene < SceneManager.sceneCount)
            SceneManager.LoadScene(currentScene + 1);
        else
            SceneManager.LoadScene(1);

    }

}
