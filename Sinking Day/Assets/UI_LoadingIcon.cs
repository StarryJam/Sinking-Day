using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LoadingIcon : MonoBehaviour {

    public float rotateSpeed = 360;
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
	}
}
