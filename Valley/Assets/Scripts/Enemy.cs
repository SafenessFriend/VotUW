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

        if (Vector3.Distance(transform.position, target.position) > 0.1f && Vector3.Distance(transform.position, target.position) < 2.5f)
        {
            if (target.transform.position.x > transform.position.x)
            {
                velocity.x = moveSpeed;
                anim.SetBool("right", true);
                anim.SetBool("left", false);

            }
            else if (target.transform.position.x < transform.position.x)
            {
                velocity.x = -moveSpeed;
                anim.SetBool("left", true);
                anim.SetBool("right", false);

            }
        }
        else
        {
            velocity.x = 0;
        }

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            
            anim.SetBool("attack", true);
            velocity.x = 0;

        }
        else if (Vector3.Distance(transform.position, target.position) > 1f)
        {
            anim.SetBool("attack", false);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }

}
