using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {

    // game object for camera to follow
    GameObject targetObj;


    /** SMOOTHING VARIABLES **/
    public bool smoothOn = true;
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

    // camera lock offset
    public float cameraLockOffset = 2f;

    // Use this for initialization
    void Start () {
        targetObj = GameObject.FindWithTag("Player");
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
            }
        }

        
        if (smoothOn)
        {
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
        }
        else
        {
            // follow player without smoothing
            posX = desiredPosition.x;
            posY = desiredPosition.y;
        }


        // find active boundary (that player is inside)
        GameObject boundary = GameObject.Find("Boundary");
        if (boundary)
        {
            float offset = Camera.main.aspect * Camera.main.orthographicSize;
            // keep camera position inside the boundary
            posX = Mathf.Clamp(posX, boundary.GetComponent<BoxCollider2D>().bounds.min.x + offset, boundary.GetComponent<BoxCollider2D>().bounds.max.x - offset);
            posY = Mathf.Clamp(posY, boundary.GetComponent<BoxCollider2D>().bounds.min.y, boundary.GetComponent<BoxCollider2D>().bounds.max.y);
        }



        // update position
        transform.position = new Vector3(posX, posY, transform.position.z);

      


        // DRAW FOCUS DEBUG
        if (drawFocus){
            DrawFocus(posX, posY);
        }


    }





    void DrawFocus (float x, float y)
    {
        // draw camera focus lines
        // x focus
        Vector3 start = new Vector3(x - cameraOffset.x + focusX, y * 100, 0);
        Vector3 end = new Vector3(x - cameraOffset.x + focusX, y * -100, 0);
        Debug.DrawLine(start, end, Color.yellow);
        start = new Vector3(x - cameraOffset.x - focusX, y * 100, 0);
        end = new Vector3(x - cameraOffset.x - focusX, y * -100, 0);
        Debug.DrawLine(start, end, Color.yellow);
        // y focus
        start = new Vector3(x * 100, y + cameraOffset.y + focusYAbove, 0);
        end = new Vector3(x * -100, y + cameraOffset.y + focusYAbove, 0);
        Debug.DrawLine(start, end, Color.red);
        start = new Vector3(x * 100, y + cameraOffset.y - focusYBelow, 0);
        end = new Vector3(x * -100, y + cameraOffset.y - focusYBelow, 0);
        Debug.DrawLine(start, end, Color.red);
    }
}
