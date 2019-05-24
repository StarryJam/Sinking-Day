using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroryOnTime : MonoBehaviour {

    public float lifeTime = 1;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, lifeTime);
	}
	
}
