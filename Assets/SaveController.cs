using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private static SaveController _instance;
    public static SaveController Instance { get { return _instance; } }


    public GameObject local;
    public GameObject remote;
    public List<string> localSequence = new List<string>();
    public List<string> remoteSequence = new List<string>();

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

    public void SaveSequence()
    {
        localSequence = local.GetComponent<GameController>().sequenceTags;
        remoteSequence = remote.GetComponent<OpponentController>().sequence;
        for (int i = 0; i < 7; i++)
        {
            PlayerPrefs.SetString("playerSequence[" + i + "]", localSequence[i]);
            PlayerPrefs.SetString("remoteSequence[" + i + "]", remoteSequence[i]);
        }
    }

    public void LoadSequence()
    {
        for (int i = 0; i < 7; i++)
        {
            localSequence.Add(PlayerPrefs.GetString("playerSequence[" + i + "]"));
            remoteSequence.Add(PlayerPrefs.GetString("remoteSequence[" + i + "]"));
        }
    }

}
