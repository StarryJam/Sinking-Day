using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UnitInfoHud : MonoBehaviour {

    public Text unitNameText;
    public Text healthText;
    public Text attackText;
    public Unit unit;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(unit!=null)
            UpdateInfo();
	}

    public void UpdateInfo()
    {
        healthText.text = "Health: " + unit.currentHealth.ToString() + " / " + unit.maxHealth.ToString();
        attackText.text = "Attack:  " + unit.attackDamage.ToString();
    }
}
