using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

    public bool isOn;
    public GameObject light;
    public AudioSource sound;
    public bool failSafe;

    void Start()
    {
        light.SetActive(false);
        isOn = false;
        failSafe = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isOn && !failSafe)
            {
                failSafe = true;
                light.SetActive(true);
                isOn = true;
                sound.Play();
                StartCoroutine(FailSafe());
            }
            if (isOn && !failSafe)
            {
                failSafe = true;
                light.SetActive(false);
                isOn = false;
                sound.Play();
                StartCoroutine(FailSafe());
            }
        }
    }

    IEnumerator FailSafe()
    {
        yield return new WaitForSeconds(0.25f);
        failSafe = false;
    }
}
