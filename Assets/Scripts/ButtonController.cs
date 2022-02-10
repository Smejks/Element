using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{


    void Start()
    {
        
    }

    public void Fight()
    {
        SaveController.Instance.SaveSequence();
    }
}
