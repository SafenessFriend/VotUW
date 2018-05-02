using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Controller2D)) ]
public class Player : MonoBehaviour {

    public float jumpHeight = 2;
    public float timeToJumpApex = 0.4f;
    public float moveSpeed = 4f;
        
    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    
    Controller2D controller;

    Animator anim;

    void Start ()
    {
        controller = GetComponent<Controller2D>();
        anim = GetComponent<Animator>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }
	
	// Update is called once per frame
	void Update () {

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (controller.collisions.below)
        {
            anim.SetBool("grounded", true);
        }
        else
        {
            anim.SetBool("grounded", false);
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
            anim.SetTrigger("jump");
        }

        velocity.x = input.x * moveSpeed;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            anim.SetBool("runR", true);
            anim.SetBool("runL", false);

        }
        else if ((Input.GetAxisRaw("Horizontal") == -1))
        {
            anim.SetBool("runR", false);
            anim.SetBool("runL", true);
        }
        else
        {
            anim.SetBool("runR", false);
            anim.SetBool("runL", false);
        }

        if (velocity.x != 0)
        {
            anim.SetInteger("state", 1);
        }
        else
        {
            anim.SetInteger("state", 0);
        }

        anim.SetFloat("playerSpeed", velocity.x);
        anim.SetFloat("fallSpeed", velocity.y);
    }
}