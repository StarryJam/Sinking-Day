using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour {

    Skill skill;
    public Image icon;
    public GameObject coolDownText;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void UpdateButton(Skill _skill)
    {
        skill = _skill;
        if (skill.restCooldown != 0)
        {
            coolDownText.SetActive(true);
            coolDownText.transform.GetChild(0).GetComponent<Text>().text = skill.restCooldown.ToString();
        }
        else
            coolDownText.SetActive(false);

        icon.sprite = skill.skillIcon;
        button.onClick.AddListener(skill.StartSpelling);
    }
    
}
