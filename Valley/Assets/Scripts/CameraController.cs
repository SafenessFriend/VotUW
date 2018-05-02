using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // target to reference location
    public GameObject targetObj;

    // camera offset relative to target object position
    public Vector3 cameraPosition;

    // camera target position
    Vector3 offset;



	// Use this for initialization
	void Start () {
        // set target to object location
        offset = transform.position - targetObj.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //move camera to target position, then adjust relative camera position
        transform.position = targetObj.transform.position + offset + cameraPosition;
    }
}
