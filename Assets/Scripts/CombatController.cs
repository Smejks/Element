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

    bool fightOver;

    AudioController audioController;

    void Start()
    {
        fightOver = false;
        playerSequence = SaveData.Instance.localSequence;
        opponentSequence = SaveData.Instance.remoteSequence;
        audioController = FindObjectOfType<AudioController>();
        StartCoroutine("DelaySequence", 0);
    }

    void Update()
    {
        if (fightOver) RenderButton();

    }

    private void RenderButton() {
        if (score > 0) {
            gc.GetComponentInChildren<GameController>().ActivateReturnButton("YOU WIN!");
        }
        else if (score < 0) {
            gc.GetComponentInChildren<GameController>().ActivateReturnButton("YOU LOSE!");
        }
        else {
            gc.GetComponentInChildren<GameController>().ActivateReturnButton("TIE!");
        }
        //SaveData.Instance.ClearSequence();
    }

    IEnumerator DelaySequence(int i)
    {
        DrawPlayerSequence(i);
        DrawOpponentSequence(i);
        ResolveCombat(i);

        yield return new WaitForSeconds(0.5f);
        i++;
        if (i < 7)
            StartCoroutine("DelaySequence", i);
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

    public void ResolveCombat(int i) {
        if (opponentSequence[i] == playerSequence[i]) {
            results.Add(Instantiate(indicators[0], new Vector2(transform.position.x + i * 2.2f, transform.position.y - resultOffsetY), Quaternion.identity, transform));
            audioController.PlaySFX(5);
        }
        else if (opponentSequence[i] == "Grass" && playerSequence[i] == "Water") {
            results.Add(Instantiate(indicators[1], new Vector2(transform.position.x + i * 2.2f, transform.position.y - resultOffsetY), Quaternion.identity, transform));
            score--;
            audioController.PlaySFX(6);
        }
        else if (opponentSequence[i] == "Grass" && playerSequence[i] == "Fire") {
            results.Add(Instantiate(indicators[2], new Vector2(transform.position.x + i * 2.2f, transform.position.y - resultOffsetY), Quaternion.identity, transform));
            score++;
            audioController.PlaySFX(4);
        }
        else if (opponentSequence[i] == "Water" && playerSequence[i] == "Fire") {
            results.Add(Instantiate(indicators[1], new Vector2(transform.position.x + i * 2.2f, transform.position.y - resultOffsetY), Quaternion.identity, transform));
            score--;
            audioController.PlaySFX(6);
        }
        else if (opponentSequence[i] == "Water" && playerSequence[i] == "Grass") {
            results.Add(Instantiate(indicators[2], new Vector2(transform.position.x + i * 2.2f, transform.position.y - resultOffsetY), Quaternion.identity, transform));
            score++;
            audioController.PlaySFX(4);
        }
        else if (opponentSequence[i] == "Fire" && playerSequence[i] == "Grass") {
            results.Add(Instantiate(indicators[1], new Vector2(transform.position.x + i * 2.2f, transform.position.y - resultOffsetY), Quaternion.identity, transform));
            score--;
            audioController.PlaySFX(6);
        }
        else if (opponentSequence[i] == "Fire" && playerSequence[i] == "Water") {
            results.Add(Instantiate(indicators[2], new Vector2(transform.position.x + i * 2.2f, transform.position.y - resultOffsetY), Quaternion.identity, transform));
            score++;
            audioController.PlaySFX(4);
        }

        Invoke("DisplayResults", 0.5f);
    }

    private void DisplayResults() {
        if (results.Count == 7) {
            fightOver = true;
            if (score > 0) {
                audioController.GetComponent<AudioSource>().Stop();
                audioController.PlaySFX(7);
                //Add score to player
            }
            else if (score < 0) {
                audioController.PlaySFX(9);
                //Add score to opponent
            }
            else {
                audioController.PlaySFX(8);
                //Initiate tiebreaker
            }
        }
    }
}
