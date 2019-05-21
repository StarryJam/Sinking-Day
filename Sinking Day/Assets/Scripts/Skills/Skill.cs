using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using QFramework.EventID;

public class Skill : MonoBasedOnTurn {

    public enum SkillType
    {
        passive,
        toUnit,
        toArea
    }

    public string skillName;
    public Sprite skillIcon;
    public SkillType type;
    public int coolDown = 1;
    public float damage = 10;
    public int spellRange = 5;
    public int influenceRange = 5;
    public bool isToEnemy;
    public bool hasProjectile;
    public Projectile Projectile;
    public float ProjectileSpeed;

    public GameObject target;
    private Unit speller;

    public int restCooldown = 0;

    private enum SkillState
    {
        holding,
        ReadyToSpell
    }

	// Use this for initialization
	void Start () {
        speller = transform.parent.GetComponent<Unit>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartSpelling()
    {
        speller.GetComponent<UnitOfPlyer>().ReadyToSpell(this);
    }

    public void ChooseTarget()
    {

    }

    public void Spell(Unit target)//单体技能释放
    {
        if (restCooldown == 0)//冷却完成才能释放
        {
            if (hasProjectile && Projectile != null)
            {
                Projectile currentProjectile;
                if (speller.projectilePos != null)
                    currentProjectile = Instantiate(Projectile, speller.projectilePos);
                else
                    currentProjectile = Instantiate(Projectile, transform);
                if (isToEnemy)
                    currentProjectile.SetProjectile(target.gameObject, ProjectileSpeed, damage);
            }
            restCooldown = coolDown;
            UIManager.UpdateSelectInformation();
        }
    }

    public void StopSpelling()
    {
        
    }

    public override void AtTurnStart()
    {
        CoolDown(1);
    }

    public void CoolDown(int value)
    {
        if (restCooldown > 0)
        {
            restCooldown -= value;
            if (restCooldown < 0)
                restCooldown = 0;
        }
    }
}
