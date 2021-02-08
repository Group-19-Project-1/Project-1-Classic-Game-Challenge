using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTracker : MonoBehaviour
{
    public AudioSource[] soundFX;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            soundFX[0].Play();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            soundFX[0].Play();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            soundFX[0].Play();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            soundFX[0].Play();
        }
    }
}
