using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int speed = 20;
    public Camera myCam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        updateMovement();
        updateCameraPosition();
	}

    private void updateCameraPosition()
    {
        Vector3 curPosition = transform.position;
        curPosition.y += 25;

        myCam.transform.position = curPosition;
    }

    private void updateMovement()
    {
        transform.eulerAngles = new Vector3(0,myCam.transform.eulerAngles.y,0);

        transform.Translate(new Vector3(Input.GetAxis("Horizontal")*speed,
            0, 
            Input.GetAxis("Vertical")*speed));
    }
}
