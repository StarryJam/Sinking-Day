using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitOfEnemy : Unit
{

    new void Start()
    {
        base.Start();
        EnemyAI.enemyUnits.Add(this);
    }

}