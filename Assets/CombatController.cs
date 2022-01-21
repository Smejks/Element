using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject gc;
    public GameObject oc;
    List<GameObject> playerSequence = new List<GameObject>();
    List<GameObject> opponentSequence = new List<GameObject>();

    void Start()
    {
        playerSequence = gc.GetComponent<GameController>().sequence;
        opponentSequence = oc.GetComponent<OpponentController>().sequence;
        //ResolveCombat();



    }

    void Update()
    {
     

    }

    //IEnumerator ResolveCombat()
    //{

    //    for (int i = 0; i < 7; i++)
    //    {

    //    }

    //}




}
