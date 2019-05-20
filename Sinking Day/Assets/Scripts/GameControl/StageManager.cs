using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using QFramework.EventID;

public class StageManager : MonoBehaviour {

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
        //结算Dot伤害

        //冷却技能
        QEventSystem.SendEvent(GameEventID.Skill.coolDown, 1);
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
        StartCoroutine(AIMoveing());
    }

    //public void EndTurn(int key, params object[] param)//回合结束
    //{
    //    Debug.Log("回合结束");
    //}

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
