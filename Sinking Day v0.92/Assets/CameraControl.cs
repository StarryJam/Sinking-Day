using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    private Transform cmrTrans;
    private Vector3 cmrAng;
    // Use this for initialization
    void Start () {
        cmrTrans = Camera.main.transform;
        cmrAng = cmrTrans.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
        CameraRotate();
	}

    public void CameraRotate()
    {
        float y = Input.GetAxis("Mouse X");
        float x = Input.GetAxis("Mouse Y");
        cmrAng.x -= x / 10;
        cmrAng.y += y / 10;
        cmrTrans.eulerAngles = cmrAng;
    }
}
