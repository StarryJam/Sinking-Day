using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour {

    private GameObject mainCamera;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.rotation=mainCamera.transform.rotation;
	}
}
