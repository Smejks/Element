using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class RightClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject GameController;
    AudioController audioController;

    int index;
    public void Start()
    {
        audioController = FindObjectOfType<AudioController>();
        index = transform.GetSiblingIndex();
    }
    public void OnPointerClick(PointerEventData data)
    {

        switch (data.button)
        {
            case PointerEventData.InputButton.Left:
                GameController.GetComponent<GameController>().AddElement(index);
                audioController.PlaySFX(index);
                break;
            case PointerEventData.InputButton.Right:
                GameController.GetComponent<GameController>().RemoveElement();
                audioController.PlaySFX(3);
                break;
        }

    }
}