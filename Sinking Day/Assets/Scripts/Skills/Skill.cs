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
    public RFX1_Target Projectile;

    public GameObject target;
    private Unit speller;

    public int restCooldown = 0;

    private enum SkillState
    {
        holding,
        ReadyToSpell
    }

	void Start () {
        speller = transform.parent.GetComponent<Unit>();
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
                RFX1_Target currentProjectile;
                if (speller.projectilePos != null)
                    currentProjectile = Instantiate(Projectile, speller.projectilePos);
                else
                {
                    currentProjectile = Instantiate(Projectile, transform.position, Quaternion.identity);
                }
                if (isToEnemy)
                {
                    currentProjectile.transform.LookAt(target.hitPos);
                    currentProjectile.Target = target.hitPos.gameObject;
                }
                currentProjectile.SetProjectile(target.gameObject, damage);
            }
            restCooldown = coolDown;
            UIManager.UpdateSelectInformation();
        }
    }

    public void Spell(Vector3 pos)//对地面释放
    {

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
