using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBasedOnTurn {

    public static EnemyAI enemyAISingleton;
    public static List<UnitOfEnemy> enemyUnits;

    new public void Awake()
    {
        base.Awake();
        enemyAISingleton = this;
        enemyUnits = new List<UnitOfEnemy>();
    }

    public void Start()
    {

    }
    


    public IEnumerator AIAct()
    {
        yield return new WaitForEndOfFrame();
        foreach (var enemy in enemyUnits)
        {
            enemy.ChooseTarget();
            yield return StartCoroutine(enemy.MoveToTarget());//向目标移动
            if (enemy.IsTargetInRange(enemy.target, enemy.attackRange))//若在攻击范围内发动攻击
                yield return StartCoroutine(enemy.Attack(enemy.target));
        }
    }


}
