using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour {

    public Button moveBtn;
    public Button attckBtn;
    public GameObject skillButtonContiner;
    public List<SkillButton> skillButtons;


	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
        //UIManager.skillPanel = this;
        foreach(var button in skillButtonContiner.GetComponentsInChildren<SkillButton>())
        {
            skillButtons.Add(button);
        }
	}
	
    public void UpdatePanel()
    {
        if (PointerEvent.selected != null)
        {
            if(PointerEvent.selected.GetComponent<Unit>() != null)
            {
                Unit unit = PointerEvent.selected.GetComponent<Unit>();
                for (int i = 0; i < unit.skills.Count; i++)
                {
                    skillButtons[i].UpdateButton(unit.skills[i]);
                }
            }
        }
    }

}
