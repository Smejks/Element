using SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{


    public List<GameObject> elements = new List<GameObject>();
    public List<string> sequenceTags = new List<string>();
    public List<GameObject> sequenceGameObjects = new List<GameObject>();
    public GameObject Button;
    public TMP_Text ButtonText;
    
    void Start()
    {
        Button.SetActive(false);

        //draw unused tiles
        for (int i = 0; i < 7; i++)
        {
            Instantiate(elements[3], new Vector2(transform.position.x + i * 2.2f, transform.position.y), Quaternion.identity);
        }

        


    }

    public void Update()
    {
        if (sequenceTags.Count == 7)
            ActivateFightButton();
        else
            Button.SetActive(false);
    }

    public void AddElement(int element)
    {
        if (sequenceTags.Count < 7)
        {
            sequenceGameObjects.Add(Instantiate(elements[element], new Vector2(transform.position.x + sequenceTags.Count * 2.2f, transform.position.y), Quaternion.identity));
            sequenceTags.Add(elements[element].tag);
        }
    }

    public void RemoveElement()
    {
        //Remove game object from world and from list of gameobjects, also remove tag from list of tags
        if (sequenceTags.Count > 0)
        {
            Destroy(sequenceGameObjects[sequenceTags.Count - 1]);
            sequenceGameObjects.RemoveAt(sequenceTags.Count - 1);
            sequenceTags.RemoveAt(sequenceTags.Count - 1);
        }
    }

    public void ActivateFightButton()
    {
        Button.SetActive(true);
    }
    
    public void ActivateReturnButton(string message)
    {
        Button.SetActive(true);
        ButtonText.text = message;
    }

    public void ConfirmSequence()
    {
        PlayerGameData playerData = new PlayerGameData(User.data.screenName);
        playerData.sequence = sequenceTags;
        SaveManager.SaveObject($"games/{User.activeGame.gameID}/{User.data.screenName}", playerData);
    }
}
