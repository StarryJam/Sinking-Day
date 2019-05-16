using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour {

    private GameObject camera;

	// Use this for initialization
	void Start () {
        camera = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation=camera.transform.rotation;
	}
}
