using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillOnArea : Skill {

    private GameObject SkillUI;
    public float range = 4;
    public GameObject SkillUIPrefeb;


    override public void StartCast()
    {
        if (PointerEvent.isCasting)
            return;
        if (timer <= 0)
        {
            Cursor.visible = false;
            SkillUIPrefeb = (GameObject)Resources.Load("UI/RangeCursorOnMap");
            SkillUI = Instantiate(SkillUIPrefeb);
            SkillUI.transform.localScale = new Vector3(range, range, range);
            GameObject.Find("SkillPlane").transform.GetChild(0).gameObject.SetActive(true);
        }
        base.StartCast();
    }

    override public void Casting()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("SkillPlane"));
        if (isCollider)
        {
            SkillUI.transform.position = hit.point;
        }
    }

    override public void FinishCast()
    {
        base.FinishCast();
        Destroy(SkillUI);
        GameObject.Find("SkillPlane").transform.GetChild(0).gameObject.SetActive(false);
    }

    override public void StopCast()
    {
        base.StopCast();
        Destroy(SkillUI);
        GameObject.Find("SkillPlane").transform.GetChild(0).gameObject.SetActive(false);
    }
}
