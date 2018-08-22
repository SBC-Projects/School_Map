using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraLookScript : MonoBehaviour {
    public float RotSpeed = 6f;
    public float MouseSensitivity;
    float rotX;
    float rotY;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.rotation = Quaternion.Euler(-rotY, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, rotX, 0f);

        rotY = Mathf.Clamp(rotY, -90f, 90f);

        rotX += Input.GetAxis("Mouse X") * RotSpeed;
        rotY += Input.GetAxis("Mouse Y") * RotSpeed;
    }
}
