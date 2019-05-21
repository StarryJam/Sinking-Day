using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeBtn : MonoBehaviour {

    public UI_SkillTreePanel panel;
    public Skill skill;
    public Image icon;
    private Button button;
    public Button nextBtn;

    private void Start()
    {
        button = GetComponent<Button>();
        SetButton();
        panel = transform.parent.parent.GetComponent<UI_SkillTreePanel>();
    }

    public void SetButton()
    {
        icon.sprite = skill.skillIcon;
        button.onClick.AddListener(LearnSkill);
    }

    private void LearnSkill()
    {
        if (panel.unit.SkillPoint > 0)
        {
            button.interactable = false;
            if (nextBtn != null)
                nextBtn.interactable = true;
            panel.unit.LearnSkill(skill);
        }
    }
}
