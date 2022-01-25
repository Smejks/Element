using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject gc;
    public GameObject oc;
    public List<string> playerSequence = new List<string>();
    public List<string> opponentSequence = new List<string>();
    public List<GameObject> indicators = new List<GameObject>();
    public List<GameObject> results = new List<GameObject>();

    public float offsetY = 8f;
    public float resultOffsetY = 160.5f;
    public int score;


    void Start()
    {
        //SaveController.Instance.LoadSequence();
        playerSequence = SaveController.Instance.localSequence;
        opponentSequence = SaveController.Instance.remoteSequence;


        DelaySequence();

    }

    void Update()
    {
        if (results.Count == 7)
        {
            if (score > 0)
            gc.GetComponentInChildren<GameController>().ActivateReturnButton("YOU WIN!");
            if (score < 0)
                gc.GetComponentInChildren<GameController>().ActivateReturnButton("YOU LOSE!");
            if (score == 0)
                gc.GetComponentInChildren<GameController>().ActivateReturnButton("TIE!");

            SaveController.Instance.ClearSequence();
            CalculateWinner();
        }
    }

    void DelaySequence()
    {

        for (int i = 0; i < 7; i++)
        {
            DrawPlayerSequence(i);
            DrawOpponentSequence(i);
            ResolveCombat(i);
        }
    }

    public void DrawPlayerSequence(int i)
    {

        switch (playerSequence[i])
        {
            case "Grass":
                Instantiate(gc.GetComponent<GameController>().elements[0], new Vector2(transform.position.x + i * 2.2f, transform.position.y - offsetY), Quaternion.identity, transform);
                break;
            case "Water":
                Instantiate(gc.GetComponent<GameController>().elements[1], new Vector2(transform.position.x + i * 2.2f, transform.position.y - offsetY), Quaternion.identity, transform);
                break;
            case "Fire":
                Instantiate(gc.GetComponent<GameController>().elements[2], new Vector2(transform.position.x + i * 2.2f, transform.position.y - offsetY), Quaternion.identity, transform);
                break;
            default:
                break;
        }





    }

    public void DrawOpponentSequence(int i)
    {

        switch (opponentSequence[i])
        {
            case "Grass":
                Instantiate(gc.GetComponent<GameController>().elements[0], new Vector2(transform.position.x + i * 2.2f, transform.position.y), Quaternion.identity, transform);
                break;
            case "Water":
                Instantiate(gc.GetComponent<GameController>().elements[1], new Vector2(transform.position.x + i * 2.2f, transform.position.y), Quaternion.identity, transform);
                break;
            case "Fire":
                Instantiate(gc.GetComponent<GameController>().elements[2], new Vector2(transform.position.x + i * 2.2f, transform.position.y), Quaternion.identity, transform);
                break;
            default:
                break;
        }
    }

    public void ResolveCombat(int i)
    {
        if (opponentSequence[i] == playerSequence[i])
            results.Add(Instantiate(indicators[0], new Vector2(transform.position.x + i * 2.2f, transform.position.y - resultOffsetY), Quaternion.identity, transform));

        else if (opponentSequence[i] == "Grass" && playerSequence[i] == "Water")
        {
            results.Add(Instantiate(indicators[1], new Vector2(transform.position.x + i * 2.2f, transform.position.y - resultOffsetY), Quaternion.identity, transform));
            score--;
        }
        else if (opponentSequence[i] == "Grass" && playerSequence[i] == "Fire")
        {
            results.Add(Instantiate(indicators[2], new Vector2(transform.position.x + i * 2.2f, transform.position.y - resultOffsetY), Quaternion.identity, transform));
            score++;
        }
        else if (opponentSequence[i] == "Water" && playerSequence[i] == "Fire")
        {
            results.Add(Instantiate(indicators[1], new Vector2(transform.position.x + i * 2.2f, transform.position.y - resultOffsetY), Quaternion.identity, transform));
            score--;
        }
        else if (opponentSequence[i] == "Water" && playerSequence[i] == "Grass")
        {
            results.Add(Instantiate(indicators[2], new Vector2(transform.position.x + i * 2.2f, transform.position.y - resultOffsetY), Quaternion.identity, transform));
            score++;
        }
        else if (opponentSequence[i] == "Fire" && playerSequence[i] == "Grass")
        {
            results.Add(Instantiate(indicators[1], new Vector2(transform.position.x + i * 2.2f, transform.position.y - resultOffsetY), Quaternion.identity, transform));
            score--;
        }
        else if (opponentSequence[i] == "Fire" && playerSequence[i] == "Water")
        {
            results.Add(Instantiate(indicators[2], new Vector2(transform.position.x + i * 2.2f, transform.position.y - resultOffsetY), Quaternion.identity, transform));
            score++;
        }
    }

    public void CalculateWinner()
    {

        if (score > 0)
            Debug.Log("You Win!");
        if (score < 0)
            Debug.Log("You lose!");
        if (score == 0)
            Debug.Log("It's a tie!");

    }


}
