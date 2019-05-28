using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using QFramework.EventID;

public class StageManager : MonoBehaviour {

    public static int numOfTurn = 0;//回合数
    public static StageManager stageManager;
    public static TurnStage turnStage;

    public enum TurnStage
    {
        playMoveing,
        AIMoving,
    }

    

    private void Awake()
    {
        stageManager = this;
    }

    void Start()
    {
        StartTurn();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void StartTurn()//回合开始
    {
        numOfTurn++;

        //结算Dot伤害

        //冷却技能
        QEventSystem.SendEvent(GameEventID.TurnManager.turnStart);
        UIManager.UpdateSelectInformation();

        turnStage = TurnStage.playMoveing;
        PlayerMoving();
    }

    private void PlayerMoving()//玩家行动
    {
        //
    }

    public void PlayerEnd()
    {
        turnStage = TurnStage.AIMoving;
        UnitSpawner.spawner.SpawnUnits();
        StartCoroutine(AIMoveing());
    }

    private IEnumerator AIMoveing()//AI行动
    {
        yield return(StartCoroutine(EnemyAI.enemyAISingleton.AIAct()));
        AIEnd();
    }

    public void AIEnd()
    {
        EndTurn();
    }

    private void EndTurn()
    {
        StartTurn();
    }
    
}
