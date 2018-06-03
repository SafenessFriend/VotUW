using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour {

    BoxCollider2D boundaryManager;
    GameObject player;
    public GameObject boundary;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        boundaryManager = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {

        // check player exists
        if (player != null)
        {
            if (isContains())
                boundary.SetActive(true);
            else
                boundary.SetActive(false);
        }
    }

    bool isContains()
    {
        if (boundaryManager.bounds.min.x < player.transform.position.x &&
           player.transform.position.x < boundaryManager.bounds.max.x &&
           boundaryManager.bounds.min.y < player.transform.position.y &&
           player.transform.position.y < boundaryManager.bounds.max.y)
            return true;
        else
            return false;
    }


}

