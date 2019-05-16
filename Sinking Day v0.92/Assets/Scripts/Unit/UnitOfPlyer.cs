using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitOfPlyer : Unit {

    

	// Use this for initialization
	new void Start () {
        base.Start();
        camp = Camp.friend;
	}
	
    

	new void LateUpdate () {
        base.LateUpdate();
        if (StageManager.turnStage == StageManager.TurnStage.playMoveing)
        {
            ControlAndAct();
        }
        else
        {
            ChangingState(UnitState.holding);
        }
    }
    

    new public void ChangingState(UnitState _state)
    {
        base.ChangingState(_state);
        if (state == UnitState.readyToAttack)
        {
            CheakingRange(attackRange);
        }
        else
        {
            UIManager.HideUI(rangeCursorUI);
        }
    }

    public void ReadyToMove()
    {
        ChangingState(UnitState.readyToMove);
    }

    public void ReadyToAttack()
    {
        ChangingState(UnitState.readyToAttack);
    }



    public void ControlAndAct()
    {
        //UI




        if (state == UnitState.readyToMove)
        {
            if (!PointerEvent.isOnUI)//判断鼠标是否点击在UI上
            {
                ChoosingPath();
                if (Input.GetMouseButtonUp(0) && !isMoving)
                {
                    Move();
                }
                if (Input.GetMouseButtonUp(1))
                {
                    ChangingState(UnitState.holding);
                }
            }
        }
        else if (state == UnitState.readyToAttack)
        {
            if (Input.GetMouseButtonDown(0) && PointerEvent.isOnUnit)
            {
                Attack(PointerEvent.pointerOnObj.GetComponent<Unit>());
            }
            if (Input.GetMouseButtonDown(1))
            {
                ChangingState(UnitState.holding);
            }
        }

    }


    public void CheakingRange(int range)
    {
        int rangeValue = range * 2 + 1;
        rangeCursorUI.transform.localScale = new Vector3(rangeValue, rangeValue, rangeValue);
        UIManager.DisplayUI(rangeCursorUI);
    }
    
}
