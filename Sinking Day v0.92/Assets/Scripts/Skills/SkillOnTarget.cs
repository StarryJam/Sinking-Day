using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillOnTarget : Skill {
    
    public bool toEnemy = true;//true为以敌方为目标false为以友方为目标
    private string targetTag;
    protected GameObject target;


    override public void StartCast()
    {
        if (PointerEvent.isCasting)
            return;

        base.StartCast();
        if (timer <= 0)
        {
            Cursor.SetCursor(UIManager.onPointPointer, Vector2.zero, CursorMode.Auto);
        }
    }

    override public void CastSkill()
    {
        GameObject hitObject = PointerEvent.hit.collider.gameObject;
        if ((toEnemy && PointerEvent.hit.collider.gameObject.tag == "Enemy") || (!toEnemy && PointerEvent.hit.collider.gameObject.tag == "Turret")) 
        {
            if (hitObject.GetComponent<SubObject>() != null)
            {
                target = hitObject.GetComponent<SubObject>().FindFather();
            }
            else
                target = PointerEvent.hit.collider.gameObject;
        }
    }
}