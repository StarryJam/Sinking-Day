using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverLoad : MonoBehaviour {

    public static float rate = 50;
    private float dataChange;
    private GameObject target;
    public GameObject effect;

    private void Start()
    {
        //effect = (GameObject)Resources.Load("Skills/OverLoad/OverLoadEffect");
        //target = GetComponent<Turret>();
        //effect = Instantiate(effect, target.transform.position, Quaternion.identity);
        //if (target.GetComponent<LaserTurret>() != null)
        //{
        //    dataChange = target.damage* (1 + rate / 100) - target.damage;
        //    target.damage += dataChange;
        //}
        //else if(target.GetComponent<LunchTurret>() != null)
        //{
        //    LunchTurret turret = this.target.GetComponent<LunchTurret>();
        //    dataChange = turret.attackRate - turret.attackRate / (1 + rate / 100);
        //    turret.attackRate -= dataChange;
        //}
    }
    
	
	void Update () {
		
	}

    private void OnDestroy()
    {
    //    for (int i = 0; i < effect.transform.childCount; i++)
    //    {
    //        effect.transform.GetChild(i).GetComponent<ParticleSystem>().Stop();
    //    }
    //    Destroy(effect, 1);
    //    if (target.GetComponent<LaserTurret>() != null)
    //    {
    //        LaserTurret turret = this.target.GetComponent<LaserTurret>();
    //        turret.damage -= dataChange;
    //    }
    //    else if (target.GetComponent<LunchTurret>() != null)
    //    {
    //        LunchTurret turret = this.target.GetComponent<LunchTurret>();
    //        turret.attackRate += dataChange;
    //    }
    }
}
