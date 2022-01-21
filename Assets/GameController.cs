using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public List<GameObject> elements = new List<GameObject>();
    public List<GameObject> sequence = new List<GameObject>();
    public GameObject Button;


    void Start()
    {
        Button.SetActive(false);

        for (int i = 0; i < 7; i++)
        {
            Instantiate(elements[3], new Vector2(transform.position.x + i * 2.2f, transform.position.y), Quaternion.identity);
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
            sequence.Add(Instantiate(elements[element], new Vector2(transform.position.x + sequence.Count * 2.2f, transform.position.y), Quaternion.identity));
        }
    }

    public void RemoveElement()
    {
        if (sequence.Count > 0)
        {
            Destroy(sequence[sequence.Count - 1]);
            sequence.RemoveAt(sequence.Count - 1);
        }
    }

    public void SaveSequence()
    {
        //Save Sequence List to JSON
    }



}
