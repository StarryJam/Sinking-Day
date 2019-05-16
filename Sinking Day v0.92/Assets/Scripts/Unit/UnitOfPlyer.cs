using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitOfPlyer : Unit,Selectee {
    

	// Use this for initialization
	new void Start () {
        base.Start();
        camp = Camp.friend;
        Test();
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
    

    public void ReadyToMove()
    {
        ChangingState(UnitState.readyToMove);
    }

    public void ReadyToAttack()
    {
        ChangingState(UnitState.readyToAttack);
        CheakingRange(attackRange);
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
        ChangingState(UnitState.holding);
    }

    private void DoAction()
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
        ChangingState(UnitState.holding);
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool isCollider = Physics.Raycast(ray, out hit, 1000);

        if (isCollider)
        {
            if (hit.collider.gameObject.GetComponent<Map_BaseCube>() != null) //判断是否在地图上
            {
                ChoosePath(hit.collider.gameObject.GetComponent<Map_BaseCube>());
            }
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
        Debug.Log("asda");
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
