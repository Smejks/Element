using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject gc;
    public GameObject oc;
    List<string> playerSequence = new List<string>();
    List<GameObject> opponentSequence = new List<GameObject>();

    void Start()
    {
        //playerSequence = gc.GetComponent<GameController>().sequence;
        opponentSequence = oc.GetComponent<OpponentController>().sequence;
        //ResolveCombat();
        PlaySequence();


        for (int i = 0; i < 7; i++)
        {
            string _seq;
            _seq = "playerSequence[" + i + "]";
            playerSequence.Add(PlayerPrefs.GetString(_seq));

        }

    }

    void Update()
    {


    }

    public void PlaySequence()
    {

        for (int i = 0; i < playerSequence.Count; i++)
        {

            Debug.Log(playerSequence[i]);

            switch (playerSequence[i])
            {
                case "Grass":
                    Instantiate(gc.GetComponent<GameController>().elements[0], new Vector2(transform.position.x + playerSequence.Count * 2.2f, transform.position.y), Quaternion.identity);
                    break;
                case "Water":
                    Instantiate(gc.GetComponent<GameController>().elements[1], new Vector2(transform.position.x + playerSequence.Count * 2.2f, transform.position.y), Quaternion.identity);
                    break;
                case "Fire:":
                    Instantiate(gc.GetComponent<GameController>().elements[2], new Vector2(transform.position.x + playerSequence.Count * 2.2f, transform.position.y), Quaternion.identity);
                    break;
                default:
                    break;
            }



            //    for (int i = 0; i < 7; i++)
            //{
            //    Instantiate(playerSequence[i], new Vector2(transform.position.x + playerSequence.Count * 2.2f, transform.position.y), Quaternion.identity);
            //}
        }
    }

    public void DrawPlayerSequence()
    {

    }

    public void ResolveCombat()
    {


    }




}
