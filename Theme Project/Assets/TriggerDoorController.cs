using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    private AudioSource open;
    private AudioSource close;

    void Start()
    {
        AudioSource[] sound = GetComponents<AudioSource>();
        open = sound[0];
        close = sound[1];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.Play("DoorOpen", 0, 0.0f);
            open.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.Play("DoorClose", 0, 0.0f);
            close.Play();
        }
    }

}
