using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // game object for camera to follow
    public GameObject targetObj;

    // offset value to track object
    Vector3 offset;

    //camera position relative to target
    public Vector3 cameraPosition;


	// Use this for initialization
	void Start () {
        offset = transform.position - targetObj.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = targetObj.transform.position + offset + cameraPosition;
	}
}
