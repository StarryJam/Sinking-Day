using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillTreePanel : MonoBehaviour {

    public float padding = 10;

    public List<UI_SkillTree> ui_SkillTrees;
    public Unit unit;

    public Text SkillPointText;

    private void Start()
    {
        UIManager.skillTreePanel = this;
        UpdateView();
        SetView(unit);
        gameObject.SetActive(false);
    }

    private void Update()
    {
            UpdateView();
    }

    public void SetView(Unit _unit)
    {
        unit = _unit;
        //for(int i = 0; i < unit.skillTrees.Count; i++)
        //{
        //    ui_SkillTrees[i].SetView(unit.skillTrees[i]);
        //}
        UpdateView();
    }
    

    public void UpdateView()
    {
        if (unit != null)
            SkillPointText.text = "Skill Point: " + unit.SkillPoint.ToString();
    }
}
