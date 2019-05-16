using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SlowFirld : SkillOnArea{
    
    public GameObject field;

    // Use this for initialization
    new public void StartCast () {
        base.StartCast();
        field = (GameObject)Resources.Load("Skills/SlowField/SlowField");
    }
    
    override public void CastSkill()
    {
        GameObject field = Instantiate(this.field, hit.point, Quaternion.identity);
        Destroy(field, stillTime);
        FinishCast();
    }
    
}
