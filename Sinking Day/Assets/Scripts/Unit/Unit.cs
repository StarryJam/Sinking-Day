using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cakeslice;

public class Unit : MonoBehaviour,Selectee  {

    public Grid map;

    /*---------基础属性----------*/
    public float maxHealth = 100;
    public float currentHealth;
    public int attackRange = 5;
    public float attackDamage = 10;
    public int moveRange = 5;

    public float speed = 10;
    /*---------基础属性----------*/


    /*-----------技能------------*/
    public List<Skill> skills; 
    /*-----------技能------------*/



    public static RaycastHit hit;
    public Canvas stateHudUI;
    public Canvas rangeCursorUI;
    public Transform statementUIPos;
    public Transform rangeCursorPos;

    protected bool isMoving = false;
    public UnitState state;
    public Camp camp;



    public void Start()
    {
        //UI初始化
        statementUIPos = transform.Find("StatementUIPos");
        rangeCursorPos = transform.Find("RangeCursorPos");
        stateHudUI = UIManager.CreateUI(UIManager.UI_UnitStateHud, statementUIPos);
        stateHudUI.GetComponent<UnitStateUI>().unit = this;
        rangeCursorUI = UIManager.CreateUI(UIManager.UI_RangeCursorOnUnit, rangeCursorPos);
        UIManager.HideUI(rangeCursorUI);
        

        map = Grid.map;
        currentHealth = maxHealth;
        state = UnitState.holding;
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
        readyToAttack
    }

    public void ChangingState(UnitState _state)
    {
        state = _state;
        if (_state == UnitState.holding)
        {
            PointerEvent.ChangingPointerState(PointerEvent.PointerState.normal);
        }
        if (_state == UnitState.readyToAttack)
        {
            PointerEvent.ChangingPointerState(PointerEvent.PointerState.choosingTarget);
        }
    }

    public void BeSelected()
    {
        PointerEvent.selected = gameObject;
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.AddComponent<Outline>();
        }
    }

    public void DeSelected()
    {
        PointerEvent.selected = null;
        ChangingState(UnitState.holding);
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject.GetComponent<Outline>());
        }
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



    public void Move()
    {
        StartCoroutine(MovePath(map.path));
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
        unit.takeDamage(attackDamage);
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

    private void Die()
    {
        Destroy(gameObject);
    }

}
