using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Enemy : MonoBehaviour {

    public Transform target;//set target from inspector instead of looking in Update
    public float speed = 3f;

    public float jumpHeight = 2;
    public float timeToJumpApex = 0.4f;
    public float moveSpeed = 4f;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;

    Controller2D controller;

    Animator anim;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        anim = GetComponent<Animator>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    void Update()
    {

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }


        ////move towards the player
        //if (Vector3.Distance(transform.position, target.position) > 0.1f)
        //{//move if distance from target is greater than 1
        //    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        //}

        velocity = new Vector3((transform.position.x - target.transform.position.x) * moveSpeed, (transform.position.y - target.transform.position.y) * moveSpeed);
        GetComponent<Rigidbody2D>().velocity = -velocity;



        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }

}
