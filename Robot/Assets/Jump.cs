using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private float velocity = 3.8f;
    private float x = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        x = Mathf.PingPong(Time.time * velocity, 6);
        transform.position = new Vector3(0, x, 0);
    }
}
