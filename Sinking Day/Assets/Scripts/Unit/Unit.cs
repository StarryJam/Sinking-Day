using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cakeslice;
using QFramework;
using QFramework.EventID;

public class Unit : MonoBasedOnTurn,Selectee  {

    public Grid map;
    
    public Transform hitPos;

    /*---------基础属性----------*/
    public int level = 1;
    public float maxHealth = 100;
    public float currentHealth;
    public int attackRange = 5;
    public float attackDamage = 10;
    public int moveRange = 5;
    public int maxActionPoint = 3;
    public int currentActionPoint;
    public int expNeedForLUP = 50;
    public int exp = 0;

    public int expGain = 10;//玩家杀死该单位获得的经验

    public float speed = 6;//在地图上的移动速度
    public float rotateSpeed = 1;//转身速度
    /*---------基础属性----------*/


    /*-----------技能------------*/
    public int SkillPoint = 5;
    public Skill normalAttack;
    public List<Skill> skills;
    public List<SkillTree> skillTrees;
    protected Skill currentSkill;
    /*-----------技能------------*/

    /*-----------特效------------*/
    private GameObject levelUpFVX;
    /*-----------特效------------*/

    [HideInInspector] public Canvas stateHudUI;
    [HideInInspector] public Transform statementUIPos;
    public Transform projectilePos;

    protected GameObject model;
    protected Animator animator;
    public UnitState state;
    public Camp camp;

    new public void Awake()
    {
        base.Awake();
    }

    public void Start()
    {
        //UI初始化
        statementUIPos = transform.Find("StatementUIPos");
        stateHudUI = UIManager.CreateUI(UIManager.ui_UnitStateHud, statementUIPos);
        stateHudUI.GetComponent<UnitStateUI>().unit = this;

        //技能初始化
        normalAttack = ((GameObject)Instantiate(Resources.Load("Prefebs/Skills/NormalAttack/NormalAttack_Skill"),transform)).GetComponent<Skill>();
        normalAttack.spellRange = attackRange;
        for(int i=0; i< skills.Count; i++)
        {
            skills[i] = Instantiate(skills[i], transform);
        }

        //特效初始化
        levelUpFVX = (GameObject)Resources.Load("Prefebs/Effect/LevelUpFVX");

        //属性初始化
        currentHealth = maxHealth;
        state = UnitState.holding;
        currentActionPoint = maxActionPoint;

        model = transform.Find("Model").gameObject;
        animator = model.GetComponent<Animator>();
        map = Grid.map;
        hitPos= transform.Find("HitPos");

    }

    public enum Camp //阵营
    {
        friend,
        enemy
    }

    public enum UnitState
    {
        holding,
        readyToMove,
        readyToAttack,
        readyToSpell,
        moving
    }

    public void ChangeState(UnitState _state)
    {
        state = _state;
    }

    public void BeSelected()
    {
        PointerEvent.selected = gameObject;
        foreach (Transform child in transform.GetComponentsInChildren<Transform>(true))
        {
            if (child.GetComponent<Renderer>() != null)
                child.gameObject.AddComponent<Outline>();
        }
        if (GetComponent<UnitOfPlyer>() != null)
        {
            UIManager.ShowSelectInformation();
        }
    }

    public void DeSelected()
    {
        PointerEvent.selected = null;
        ChangeState(UnitState.holding);
        foreach (Transform child in transform.GetComponentsInChildren<Transform>(true))
        {
            if (child.GetComponent<Outline>() != null)
                Destroy(child.gameObject.GetComponent<Outline>());
        }
        UIManager.HideSelectInformation();
    }



    public void LateUpdate () {
        
    }

    //public void ChoosingPath()
    //{
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //        bool isCollider = Physics.Raycast(ray, out hit, 1000);

    //        if (isCollider)
    //        {
    //            if (hit.collider.gameObject.GetComponent<Map_BaseCube>() != null) //判断是否在地图上
    //            {
    //                map.FindPath(transform.position, hit.collider.transform.position);
                    
    //            }
    //        }
    //}

    public void ChoosePath(Map_BaseCube destination)
    {
        map.FindPath(transform.position, PointerEvent.pointerOnObj.transform.position);
    }

    public void RemainHolding()
    {
        ChangeState(UnitState.holding);
        currentSkill = null;
        map.ClearPath();
    }

    public IEnumerator Move()
    {
        ChangeState(UnitState.moving);
        yield return StartCoroutine(MovePath(map.path));
        RemainHolding();
        animator.SetTrigger("Hold");
        map.path.Clear();
    }

     IEnumerator MovePath(List<Node> _path)
    {
        if (_path.Count != 0)
        {
            animator.SetTrigger("Run");
            List<Node> path = new List<Node>();
            for (int i = 0; i < _path.Count; i++)
            {
                path.Add(_path[i]);
            }
            if (path.Count > 0)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    yield return StartCoroutine(MoveAStep(path[i]._worldPos));
                }
            }
        }
    }

    IEnumerator MoveAStep(Vector3 toPositon)
    {
        float schedule = 0;//动画插值
        Vector3 from = transform.position;
        Vector3 to = toPositon;
        float time = Vector3.Distance(from, to) / speed;
        float scheduleSpeed = 1 / time;
        StartCoroutine(Turning(toPositon));
        while (schedule < 1)
        {
            schedule += Time.deltaTime * scheduleSpeed;
            //if (schedule > 1)
            //    schedule = 1;

            transform.position = Vector3.Lerp(from, to, schedule);
            yield return null;
        }
    }

    protected IEnumerator Turning(Vector3 target)
    {
        float schedule = 0;//动画插值
        Quaternion current_Quaternion = transform.rotation;
        transform.LookAt(target);
        Quaternion targt_Quaternion = transform.rotation;
        transform.rotation = current_Quaternion;
        while (schedule <= 1)
        {
            schedule += Time.deltaTime * rotateSpeed * 5;
            transform.rotation = Quaternion.Lerp(current_Quaternion, targt_Quaternion, schedule);
            yield return null;
        }
    }


    public IEnumerator Attack(Unit unit)
    {
        yield return StartCoroutine(Turning(unit.transform.position));
        normalAttack.Spell(unit);
        animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.5f);
    }
    
    public IEnumerator SpellSkill()
    {
        if (currentSkill.type == Skill.SkillType.toUnit)
            currentSkill.Spell(PointerEvent.pointerOnObj.GetComponent<Unit>());
        yield return StartCoroutine(Turning(PointerEvent.pointerOnObj.transform.position));
        animator.SetTrigger("Shoot");
        StartCoroutine(Turning(PointerEvent.pointerOnObj.transform.position));
        yield return new WaitForSeconds(0.5f);
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateState();
    }

    private void UpdateState()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void LevelUp()
    {
        level++;
        SkillPoint++;
        Instantiate(levelUpFVX, transform);
    }

    public override void AtTurnStart()
    {
        currentActionPoint = maxActionPoint;
    }

    public void LearnSkill(Skill skill)
    {
        skills.Add(Instantiate(skill, transform));
        UIManager.UpdateSelectInformation();
        SkillPoint--;

    }

    protected virtual void Die()
    {
        DeSelected();
        Animator dieAni = Instantiate(model, model.transform.position, transform.rotation).GetComponent<Animator>();
        dieAni.SetTrigger("Die");
        if (camp == Camp.enemy)
        {
            QEventSystem.SendEvent(GameEventID.Unit.enemyDie, expGain);
        }
        Destroy(gameObject);
    }

    public bool IsTargetInRange(Unit target, int range)
    {
        return ((Vector3.Distance(target.transform.position, transform.position) - 1) / 2 <= range);
    }
}
