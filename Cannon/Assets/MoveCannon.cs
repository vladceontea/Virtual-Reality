using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCannon : MonoBehaviour

{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

    Vector3 velocity;

    bool isGrounded;

    // Update is called once per frame

    void Update()

    {

        float x = Input.GetAxis("Horizontal");

        Vector3 move = transform.right * x;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;



        controller.Move(velocity * Time.deltaTime);

    }

}
