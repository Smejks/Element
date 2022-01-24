using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    public List<GameObject> types = new List<GameObject>();
    public List<GameObject> sequence = new List<GameObject>();
    public List<GameObject> rndSequence = new List<GameObject>();
    float time;

    void Start()
    {
        // Set Sequence to opponents ssequence saved to JSON, Sequence = JsonUtility.FromJson<OpponentSequence>(jsonString);
        
        DelaySequence();
        for (int i = 0; i < 7; i++)
        {
            Instantiate(types[3], new Vector2(transform.position.x + i * 2.2f, transform.position.y), Quaternion.identity);
        }

    }

    void Update()
    {
        time += Time.deltaTime;
    }

    public void DelaySequence()
    {
        
        for (int i = 0; i < 7; i++)
        {
            Invoke("DrawOpponentSequence", i);
        }

    }

    public void DrawOpponentSequence()
    {
        sequence.Add(Instantiate(types[Random.Range(0, 3)], new Vector2(transform.position.x + sequence.Count * 2.2f, transform.position.y), Quaternion.identity));
    }


}
