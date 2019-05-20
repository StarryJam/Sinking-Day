using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public static EnemyAI enemyAISingleton;
    public static List<UnitOfEnemy> enemyUnits;

    private void Awake()
    {
        enemyAISingleton = this;
        enemyUnits = new List<UnitOfEnemy>();
    }

    public void Start()
    {
    }


    public IEnumerator AIAct()
    {
        foreach (var enemy in enemyUnits)
        {
            enemy.ChooseTarget();
            yield return StartCoroutine(enemy.MoveToTarget());//向目标移动
            if (enemy.IsTargetInRange(enemy.attackRange))//若在攻击范围内发动攻击
                enemy.Attack(enemy.target);
        }
    }


}
