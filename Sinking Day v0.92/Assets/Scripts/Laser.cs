using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public GameObject hitEffect;
    public GameObject fireEffect;

    public void Hit(Transform turret, Transform target)
    {
        GetComponent<LineRenderer>().enabled = true;
        hitEffect.SetActive(true);
        fireEffect.SetActive(true);
        GetComponent<LineRenderer>().SetPositions(new Vector3[] {turret.position, target.position });
        hitEffect.transform.position = target.position;
        hitEffect.transform.LookAt(turret.transform);
        fireEffect.transform.position = turret.position;
    }

    public void Close()
    {
        GetComponent<LineRenderer>().enabled = false;
        hitEffect.SetActive(false);
        fireEffect.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        hitEffect = Instantiate(hitEffect);
        fireEffect = Instantiate(fireEffect);
        Close();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
