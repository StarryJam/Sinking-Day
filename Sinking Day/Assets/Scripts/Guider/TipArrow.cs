using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipArrow : MonoBehaviour {

    float timer = 0;
    private Vector3 pos;
    // Use this for initialization
    void Start () {
        pos = transform.position;
    }

    public void SetPosition(Vector3 _pos)
    {
        pos = _pos + Vector3.up * 2;
        transform.position= pos;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime * 4;
        transform.Rotate(0, 90 * Time.deltaTime, 0);
        transform.position = Vector3.Lerp(pos + new Vector3(0, 0.2f, 0), pos + new Vector3(0, -0.2f, 0), (1 + Mathf.Cos(timer)) / 2);
	}
}
