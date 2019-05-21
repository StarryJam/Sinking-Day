using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillTree : MonoBehaviour {

    private GameObject skillBtnPrefab;
    private UI_SkillTreePanel skillTreeCanvas;
    public List<UI_SkillTreeLayer> ui_SkillTreeLayers;

    public SkillTree skillTree;
    public float width;
    public float hight;
    

    // Use this for initialization
    void Start () {
        skillBtnPrefab = UIManager.skillBtn;
        skillTreeCanvas = UIManager.skillTreePanel;
	}

    public void SetView(SkillTree skillTree)
    {
        for(int i = 0; i < skillTree.layers.Count; i++)
        {
            ui_SkillTreeLayers[i].SetView(skillTree.layers[i]);
        }
    }
	
	public void UpdateUI()
    {

    }
}
