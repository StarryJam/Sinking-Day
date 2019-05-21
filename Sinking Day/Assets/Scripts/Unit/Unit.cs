using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cakeslice;

public class Unit : MonoBasedOnTurn,Selectee  {

    public Grid map;

    /*---------基础属性----------*/
    public float maxHealth = 100;
    public float currentHealth;
    public int attackRange = 5;
    public float attackDamage = 10;
    public int moveRange = 5;
    public int maxActionPoint = 3;
    public int currentActionPoint;

    public float speed = 10;//在地图上的移动速度
    /*---------基础属性----------*/


    /*-----------技能------------*/
    public int SkillPoint = 5;
    public Skill normalAttack;
    public List<Skill> skills;
    public List<SkillTree> skillTrees;
    protected Skill currentSkill;
    /*-----------技能------------*/

        
    [HideInInspector] public Canvas stateHudUI;
    [HideInInspector] public Canvas rangeCursorUI;
    [HideInInspector] public Transform statementUIPos;
    [HideInInspector] public Transform rangeCursorPos;
    [HideInInspector] public Transform projectilePos;

    protected bool isMoving = false;
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
        rangeCursorPos = transform.Find("RangeCursorPos");
        projectilePos = transform.Find("ProjectilePos");
        stateHudUI = UIManager.CreateUI(UIManager.ui_UnitStateHud, statementUIPos);
        stateHudUI.GetComponent<UnitStateUI>().unit = this;
        rangeCursorUI = UIManager.CreateUI(UIManager.ui_RangeCursorOnUnit, rangeCursorPos);
        UIManager.HideUI(rangeCursorUI);

        //技能初始化
        normalAttack = ((GameObject)Instantiate(Resources.Load("Prefebs/Skills/NormalAttack/NormalAttack_Skill"),transform)).GetComponent<Skill>();
        normalAttack.spellRange = attackRange;
        for(int i=0; i< skills.Count; i++)
        {
            skills[i] = Instantiate(skills[i], transform);
        }

        //属性初始化
        currentHealth = maxHealth;
        state = UnitState.holding;
        currentActionPoint = maxActionPoint;

        map = Grid.map;
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
        readyToSpell
    }

    public void ChangeState(UnitState _state)
    {
        state = _state;
    }

    public void BeSelected()
    {
        PointerEvent.selected = gameObject;
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.AddComponent<Outline>();
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
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject.GetComponent<Outline>());
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



    public IEnumerator Move()
    {
        yield return StartCoroutine(MovePath(map.path));
        state = UnitState.holding;
        map.path.Clear();
    }

    IEnumerator MovePath(List<Node> _path)
    {
        List<Node> path = new List<Node>();
        for(int i = 0; i < _path.Count; i++)
        {
            path.Add(_path[i]);
        }
        isMoving = true;
        if (path.Count > 0)
        {
            for (int i = 0; i < path.Count; i++)
            {
                yield return StartCoroutine(MoveAStep(path[i]._worldPos));
            }
        }
        isMoving = false;
    }

    IEnumerator MoveAStep(Vector3 toPositon)
    {
        isMoving = true;
        float schedule = 0;//动画插值
        Vector3 from = transform.position;
        Vector3 to = toPositon;
        float time = Vector3.Distance(from, to) / speed;
        float scheduleSpeed = 1 / time;
        while (schedule < 1)
        {
            schedule += Time.deltaTime * scheduleSpeed;
            if (schedule > 1)
                schedule = 1;

            transform.position = Vector3.Lerp(from, to, schedule);
            yield return new WaitForEndOfFrame();
        }
        isMoving = false;
    }



    public void Attack(Unit unit)
    {
        normalAttack.Spell(unit);
    }
    
    public void SpellSkill()
    {
        if (currentSkill.type == Skill.SkillType.toUnit)
            currentSkill.Spell(PointerEvent.pointerOnObj.GetComponent<Unit>());
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

    private void Die()
    {
        Destroy(gameObject);
    }

    public bool IsTargetInRange(Unit target, int range)
    {
        return ((Vector3.Distance(target.transform.position, transform.position) - 1) / 2 <= range);
    }
}
