using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class SaveController : MonoBehaviour
{
    private static SaveController _instance;
    public static SaveController Instance { get { return _instance; } }


    public List<string> localSequence = new List<string>();
    public List<string> remoteSequence = new List<string>();



    string nick = "bajs";
    int score = 100;


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

        localSequence = FindObjectOfType<GameController>().sequenceTags;
        remoteSequence = FindObjectOfType<OpponentController>().sequence;

        var playerdata = new PlayerSaveData(nick, localSequence, score);
        string jsonString = JsonUtility.ToJson(playerdata);
        SaveToFile("saveData", jsonString);
    }

    public static List<string> LoadSequence(string fileName)
    {
        string jsonString = SaveController.LoadFromFile(fileName);
        PlayerSaveData playerdata = JsonUtility.FromJson<PlayerSaveData>(jsonString);
        return playerdata.sequence;
    }

    public void ClearSequence()
    {
        localSequence.Clear();
        remoteSequence.Clear();
    }

    void SaveToFile(string fileName, string jsonString)
    {
        string path = @".\JSONdata\" + fileName + ".json";
        using (var stream = File.OpenWrite(path))
        {
            stream.SetLength(0);
            var bytes = Encoding.UTF8.GetBytes(jsonString);
            stream.Write(bytes, 0, bytes.Length);
        }
    }



    static string LoadFromFile(string fileName)
    {
        string path = @".\JSONdata\" + fileName + ".json";
        using (var stream = File.OpenText(path))
        {
            return stream.ReadToEnd();
        }
    }
}
