using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour
{
    private Light light;
    public AudioSource sound;
    bool soundPlayed = false;

    void Start()
    {
        light = GetComponent<Light>();
        light.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            light.enabled = true;
            if (!soundPlayed)
            {
                sound.Play();
                soundPlayed = true;
            }
        }
    }
}
