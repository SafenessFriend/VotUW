using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {

    // game object for camera to follow
    public GameObject targetObj;


    /** SMOOTHING VARIABLES **/

    // smooth delay for x & y
    public float smoothTimeX = 0.2f;
    public float smoothTimeY = 0.2f;

    // desired position of camera (at target)
    Vector3 desiredPosition;

    //camera position relative to target
    public Vector3 cameraOffset = new Vector3(0, 0, -1f);

    // variable for smooth damp reference
    Vector3 velocity;

    /** FOCUS VARIABLES **/
    // vectors for x & y focus boundaries
    public float focusX = 1f;
    public float focusYAbove = .5f;
    public float focusYBelow = .5f;
    // is the camera centered on the player
    bool isCenteredX = true;
    bool isCenteredY = true;

    // set draw focus lines
    public bool drawFocus = true;

    // Use this for initialization
    void Start () {
        transform.position = targetObj.transform.position + cameraOffset;
    }
	
	// FixedUpdate is called once per frame 
	void LateUpdate () {
        
        // get desired position for camera
        desiredPosition = targetObj.transform.position + cameraOffset;
        float posX = transform.position.x;
        float posY = transform.position.y;
        //check if player is within focus
        if (desiredPosition.x > posX + focusX || desiredPosition.x < posX - focusX) {
            // player is not centered and camera must move
            if (isCenteredX == true)
            {
                isCenteredX = false;
                //Debug.Log("OUTSIDE OF FOCUS");
            }

        }

        // check if centered
        if (isCenteredX == false)
        {
            // get smooth follow position for x axis
            posX = Mathf.SmoothDamp(transform.position.x, desiredPosition.x, ref velocity.x, smoothTimeX);
            // if camera is centered on player, isCentered is true
            if (System.Math.Round(desiredPosition.x, 1) == System.Math.Round(posX, 1))
            {
                //Debug.Log("CENTERED: " + "targetx: " + desiredPosition.x + "posX: " + posX );
                isCenteredX = true;
            } 
        }



        // get smooth follow position for y axis
        posY = Mathf.SmoothDamp(transform.position.y, desiredPosition.y, ref velocity.y, smoothTimeY);
        // update position
        transform.position = new Vector3(posX, posY, transform.position.z);

        


        if (drawFocus){
            // draw camera focus lines
            // x focus
            Vector3 start = new Vector3(posX - cameraOffset.x + focusX, posY * 100, 0);
            Vector3 end = new Vector3(posX - cameraOffset.x + focusX, posY * -100, 0);
            Debug.DrawLine(start, end, Color.yellow);
            start = new Vector3(posX - cameraOffset.x - focusX, posY * 100, 0);
            end = new Vector3(posX - cameraOffset.x - focusX, posY * -100, 0);
            Debug.DrawLine(start, end, Color.yellow);
            // y focus
            start = new Vector3(posX * 100, posY + cameraOffset.y + focusYAbove, 0);
            end = new Vector3(posX * -100, posY + cameraOffset.y + focusYAbove, 0);
            Debug.DrawLine(start, end, Color.red);
            start = new Vector3(posX * 100, posY + cameraOffset.y - focusYBelow, 0);
            end = new Vector3(posX * - 100, posY + cameraOffset.y - focusYBelow, 0);
            Debug.DrawLine(start, end, Color.red);
        }


    }
}
