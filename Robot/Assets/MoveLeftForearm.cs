using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftForearm : MonoBehaviour
{
    private float velocity = 58.0f;
    private float minDegree = 0.0f;
    private float maxDegree = 100.0f;
    private float x = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var x1 = x + velocity * Time.deltaTime;
        if (x1 > maxDegree || x1 < minDegree)
        {
            velocity *= -1;
        }
        x += velocity * Time.deltaTime;
        transform.localRotation = Quaternion.AngleAxis(x, Vector3.right);

    }
}
