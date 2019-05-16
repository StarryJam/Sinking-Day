using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_OverLoad : SkillOnTarget {

    public int speedUpRate = 50;

    public override void CastSkill()
    {
        base.CastSkill();
        if (target != null)
        {
            Debug.Log(target);
            OverLoad.rate = speedUpRate;
            OverLoad overLoad = target.AddComponent<OverLoad>();
            Destroy(overLoad, stillTime);
            FinishCast();
        }
    }
    
    override public void Casting()
    {

    }

    // Use this for initialization
    void Start () {
        toEnemy = false;
	}
	
}
