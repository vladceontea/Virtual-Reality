using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MoveKnees : MonoBehaviour
{
    private float velocity = 80.0f;
    private float minDegree = -90.0f;
    private float maxDegree = 0.0f;
    private float x = 0.0f;
    private bool isAvailable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isAvailable)
        {
            var x1 = x - velocity * Time.deltaTime;
            if (x1 > maxDegree || x1 < minDegree)
            {
                velocity *= -1;
            }
            x -= velocity * Time.deltaTime;
            transform.localRotation = Quaternion.AngleAxis(x, Vector3.right);
        }

        if (Math.Abs(x) < 1)
        {
            StartCoroutine(WaitJump());
        }

    }

    public IEnumerator WaitJump()
    {
        isAvailable = false;
        yield return new WaitForSeconds(1);
        isAvailable = true;
    }
}
