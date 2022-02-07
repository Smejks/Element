using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButtonScript : MonoBehaviour
{
    AudioController audioController;

    private void Start()
    {
        audioController = FindObjectOfType<AudioController>();
    }
    public void PlayCancelSound()
    {
        audioController.PlaySFX(3);
    }

}
