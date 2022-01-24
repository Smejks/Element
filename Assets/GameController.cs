using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public List<GameObject> elements = new List<GameObject>();
    public List<string> sequence = new List<string>();
    public List<GameObject> playedSequence = new List<GameObject>();
    public GameObject Button;
    

    void Start()
    {
        Button.SetActive(false);

        for (int i = 0; i < 7; i++)
        {
            Instantiate(elements[3], new Vector2(transform.position.x + i * 2.2f, transform.position.y), Quaternion.identity);
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            for (int i = 0; i < 7; i++)
            {
                string _seq;
                _seq = "playerSequence[" + i + "]";
                sequence.Add(PlayerPrefs.GetString(_seq));

            }
            PlaySequence();
        }

    }

    public void Update()
    {
        if (sequence.Count == 7)
            Button.SetActive(true);
        else
            Button.SetActive(false);


    }

    public void AddElement(int element)
    {
        if (sequence.Count < 7)
        {
            playedSequence.Add(Instantiate(elements[element], new Vector2(transform.position.x + sequence.Count * 2.2f, transform.position.y), Quaternion.identity));
            sequence.Add(elements[element].tag);

            Debug.Log(sequence[sequence.Count - 1]);
        }
    }

    public void RemoveElement()
    {
        if (sequence.Count > 0)
        {
            Destroy(playedSequence[sequence.Count - 1]);
            playedSequence.RemoveAt(sequence.Count - 1);
            sequence.RemoveAt(sequence.Count - 1);
        }
    }

    public void SaveSequence()
    {
        for (int i = 0; i < playedSequence.Count; i++)
        {
            PlayerPrefs.SetString("playerSequence[" + i + "]", sequence[i]);
            Debug.Log("Move saved:" + i + ":" + sequence[i]);                       
        }
    }

    public void PlaySequence()
    {
        Debug.Log("Play!");
        for (int i = 0; i < sequence.Count; i++)
        {

            switch (sequence[i])
            {
                case "Grass":
                    Instantiate(GetComponent<GameController>().elements[0], new Vector2(transform.position.x + playedSequence.Count * 2.2f, transform.position.y), Quaternion.identity);
                    break;
                case "Water":
                    Instantiate(GetComponent<GameController>().elements[1], new Vector2(transform.position.x + playedSequence.Count * 2.2f, transform.position.y), Quaternion.identity);
                    break;
                case "Fire:":
                    Instantiate(GetComponent<GameController>().elements[2], new Vector2(transform.position.x + playedSequence.Count * 2.2f, transform.position.y), Quaternion.identity);
                    break;
                default:
                    break;
            }

        }
    }



}
