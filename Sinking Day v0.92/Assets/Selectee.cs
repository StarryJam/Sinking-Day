using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using QFramework.EventID;

public class Selectee : MonoBehaviour {
    private GameObject obj;
	// Use this for initialization
	void Start () {
        obj = transform.parent.gameObject;
        QEventSystem.RegisterEvent(GameEventID.Selectee.inRange, InRange);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InRange(int key, params object[] param)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        obj.GetComponent<Renderer>().material.color = Color.blue;
    }

    private void OnCollisionExit(Collision collision)
    {
        obj.GetComponent<Renderer>().material.color = Color.white;
    }
}
