using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour {

    public enum SkillType
    {
        passive,
        toUnit,
        toArea
    }

    public string skillName;
    public Image skillIcon;
    public SkillType type;
    public int coolDown = 1;
    public float damage = 10;
    public int spellRange = 5;
    public int influenceRange = 5;
    public bool isToEnemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
