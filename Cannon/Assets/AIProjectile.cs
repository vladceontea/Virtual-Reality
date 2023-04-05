using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIProjectile : MonoBehaviour
{
    public Rigidbody projectile;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LaunchProjectile", 1.0f, 1f);
    }

    // Update is called once per frame
    void LaunchProjectile()
    {
        Rigidbody p = Instantiate(projectile, transform.position, transform.rotation);
        p.velocity = -transform.right * speed;
    }
}
