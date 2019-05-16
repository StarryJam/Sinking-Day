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
        turnStarting,
        playMoveing,
        PlayerEnding,
        AIMoving,
        AIEnding,
        turnEnding
    }

    public void Test_NextStage()
    {
        int lenth = System.Enum.GetNames(turnStage.GetType()).Length;
        int count = turnStage.GetHashCode();
        if (count == lenth - 1)
        {
            turnStage = TurnStage.turnStarting;
        }
        else
        {
            turnStage += 1;
        }
    }

    private void Awake()
    {
        stageManager = this;
    }

    void Start()
    {
        //QEventSystem.RegisterEvent(GameEventID.EndTurnButton.endTurn, EndTurn);
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void StartTurn()//回合开始
    {
        //结算Dot伤害
    }

    private void PlayerMove()//玩家行动
    {
        //
    }

    private void PlayerEnd()
    {

    }

    //public void EndTurn(int key, params object[] param)//回合结束
    //{
    //    Debug.Log("回合结束");
    //}

    private void AIMove()//AI行动
    {

    }

    private void AIEnd()
    {

    }

    private void EndTurn()
    {

    }
    
}
