using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class RightClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject GameController;
    int index;
    public void Start()
    {
        index = transform.GetSiblingIndex();
    }
    public void OnPointerClick(PointerEventData data)
    {

        switch (data.button)
        {
            case PointerEventData.InputButton.Left:
                GameController.GetComponent<GameController>().AddElement(index);
                break;
            case PointerEventData.InputButton.Right:
                GameController.GetComponent<GameController>().RemoveElement();
                break;
        }

    }
}