using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitOfEnemy : Unit
{

    public Unit target;

    new void Start()
    {
        base.Start();
        EnemyAI.enemyUnits.Add(this);
    }


    private void Update()
    {
    }

    public void ChooseTarget()
    {
        target = GameObject.FindGameObjectWithTag("UnitOfPlayer").GetComponent<Unit>();
    }

    public void ChoosePathToUnit(Unit _target)
    {
        map.FindPath(transform.position, _target.transform.position);
    }
    
    public IEnumerator MoveToTarget()
    {
        //向目标移动直到进入攻击范围或用完移动点
        ChoosePathToUnit(target);
        List<Node> temp = new List<Node>();
        for(int i = 0; i < map.path.Count; i++)
        {
            if (!IsTargetInRangeAtNode(map.path[i], attackRange - 1) && i + 1 <= moveRange)
            {
                temp.Add(map.path[i]);
            }
            else
                break;
        }
        map.path = temp;
        yield return StartCoroutine(Move());
    }

    protected override void Die()
    {
        base.Die();
        EnemyAI.enemyUnits.Remove(this);
    }

    public bool IsTargetInRangeAtNode(Node node, int range)
    {
        return (Vector3.Distance(target.transform.position, node.mapCube.transform.position) / 2 <= range);
    }





}