using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{

  
    public List<GameObject> types = new List<GameObject>();
    public List<string> sequence = new List<string>();
    public List<GameObject> rndSequence = new List<GameObject>();


    void Start()
    {
        if (SaveController.LoadSequence("saveData1") != null) {


            sequence = SaveController.LoadSequence("saveData1");
            Debug.Log("Loaded Sequence");
        }
        else {
            for (int i = 0; i < 7; i++) {
                int tag;
                tag = Random.Range(0, 3);
                if (tag == 0)
                    sequence.Add("Grass");
                else if (tag == 1)
                    sequence.Add("Water");
                else
                    sequence.Add("Fire");
            }
            Debug.Log("Randomized Sequence");
        }

        for (int i = 0; i < 7; i++)
        {
            //draw empty tiles
            Instantiate(types[3], new Vector2(transform.position.x + i * 2.2f, transform.position.y), Quaternion.identity);
        }

    }

    void Update()
    {

    }

}
