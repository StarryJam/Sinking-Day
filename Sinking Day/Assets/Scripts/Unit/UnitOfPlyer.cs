using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitOfPlyer : Unit,Selectee {
    

	// Use this for initialization
	new void Start () {
        base.Start();
	}
	
    

	new void LateUpdate () {
        base.LateUpdate();
        if (StageManager.turnStage == StageManager.TurnStage.playMoveing)
        {
            ControlAndAct();
        }
        else
        {
            RemainHolding();
        }
    }
    

    public void ReadyToMove()
    {
        PointerEvent._pointerState = PointerEvent.PointerState.choosingTarget;
        ChangingState(UnitState.readyToMove);
    }

    public void ReadyToAttack()
    {
        PointerEvent._pointerState = PointerEvent.PointerState.choosingTarget;
        ChangingState(UnitState.readyToAttack);
        CheakingRange(attackRange);
    }

    public void RemainHolding()
    {
        ChangingState(UnitState.holding);
        PointerEvent._pointerState = PointerEvent.PointerState.normal;
    }

    public void CancelAction()
    {
        if (state == UnitState.readyToMove)
        {
            map.ClearPath();
        }
        else if (state == UnitState.readyToAttack)
        {
            UIManager.HideUI(rangeCursorUI);
        }
        RemainHolding();
    }

    public void DoAction()
    {
        if (state == UnitState.readyToMove && PointerEvent.isOnMap)
        {
            Move();
            map.ClearPath();
        }
        else if (state == UnitState.readyToAttack && PointerEvent.isOnEnemy) 
        {
            Attack(PointerEvent.pointerOnObj.GetComponent<Unit>());
            UIManager.HideUI(rangeCursorUI);
        }
        RemainHolding();
    }

    public void ControlAndAct()
    {
        //UI


        if (PointerEvent.selected == gameObject)//仅在被选中时可以控制
        {
            if (state == UnitState.readyToMove)
            {
                PlayerChoosingPath();
                map.DisplayPath();
            }
            else
            {
                map.UndisplayPath();
            }
            if (!PointerEvent.isOnUI)//判断鼠标是否在UI上
            {
                if (Input.GetMouseButtonUp(0))
                {
                    DoAction();
                }
                if (Input.GetMouseButtonUp(1))
                {
                    CancelAction();
                }
            }
        }
    }

    private void PlayerChoosingPath()
    {
        map.GenerateMoveRange(transform.position, moveRange);

        if (PointerEvent.isOnMap) //判断是否在地图上
        {
            Map_BaseCube currentCube = PointerEvent.pointerOnObj.GetComponent<Map_BaseCube>();
            if (currentCube.node.state == Node.NodeState.inMoveRange || currentCube.node.state == Node.NodeState.inPath)
                ChoosePath(currentCube);
        }
    }

    public void CheakingRange(int range)
    {
        int rangeValue = range * 2 + 1;
        rangeCursorUI.transform.localScale = new Vector3(rangeValue, rangeValue, rangeValue);
        UIManager.DisplayUI(rangeCursorUI);
    }

    public void Cancel()
    {
        if (state == UnitState.readyToAttack || state == UnitState.readyToMove)
        {
            CancelAction();
        }
        else
            DeSelected();
    }

    public void MouseHit()
    {

    }
}
