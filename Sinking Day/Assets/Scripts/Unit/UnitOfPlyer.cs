﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitOfPlyer : Unit, Selectee {

    private int needAP;

    new public void Awake()
    {
        base.Awake();
        gameObject.tag = "UnitOfPlayer";
    }

    new void Start() {
        base.Start();
    }



    new void LateUpdate() {
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
        PointerEvent.ChangingPointerState(PointerEvent.PointerState.normal);
        ChangeState(UnitState.readyToMove);
    }

    public void ReadyToAttack()
    {
        PointerEvent.ChangingPointerState(PointerEvent.PointerState.choosingTarget);
        ChangeState(UnitState.readyToAttack);
        CheakRange(attackRange);
    }

    public void ReadyToSpell(Skill skill)
    {
        currentSkill = skill;
        PointerEvent.ChangingPointerState(PointerEvent.PointerState.choosingTarget);
        ChangeState(UnitState.readyToSpell);
        CheakRange(skill.spellRange);
    }

    private void ReadyToUseAP(int point)
    {
        needAP = point;
        stateHudUI.GetComponent<UnitStateUI>().UsingActionPoint(point);
    }

    private void UsingAP()
    {
        currentActionPoint -= needAP;
    }

    public void RemainHolding()
    {
        ChangeState(UnitState.holding);
        currentSkill = null;
        map.ClearPath();
        ReadyToUseAP(0);
        PointerEvent.ChangingPointerState(PointerEvent.PointerState.normal);
        UIManager.HideUI(rangeCursorUI);
    }

    public void CancelAction()
    {
        RemainHolding();
    }

    public void DoAction()
    {
        if (state == UnitState.readyToMove && PointerEvent.isOnMap)
        {
            StartCoroutine(Move());
            UsingAP();
        }
        else if (state == UnitState.readyToAttack && PointerEvent.isOnEnemy)
        {
            Attack(PointerEvent.pointerOnObj.GetComponent<Unit>());
            Debug.Log(1);
            UsingAP();
        }
        else if (state == UnitState.readyToSpell && PointerEvent.isOnUnit)
        {
            SpellSkill();
            UsingAP();
        }
        RemainHolding();
    }

    private void ControlAndAct()
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
        }
    }

    private void PlayerChoosingPath()
    {
        map.GenerateMoveRange(transform.position, moveRange * currentActionPoint);

        if (PointerEvent.isOnMap) //判断是否在地图上
        {
            Map_BaseCube currentCube = PointerEvent.pointerOnObj.GetComponent<Map_BaseCube>();
            if (currentCube.node.state == Node.NodeState.inMoveRange || currentCube.node.state == Node.NodeState.inPath)
            {
                ChoosePath(currentCube);
                int needActionPoint = map.path.Count / moveRange;
                if (map.path.Count % moveRange != 0)
                    needActionPoint++;
                ReadyToUseAP(needActionPoint);
            }
        }
    }

    public void CheakRange(int range)
    {
        int rangeValue = range * 2 + 1;
        rangeCursorUI.transform.localScale = new Vector3(rangeValue, rangeValue, rangeValue);
        UIManager.DisplayUI(rangeCursorUI);
    }

    public void Cancel()
    {
        if (state == UnitState.readyToAttack || state == UnitState.readyToMove || state == UnitState.readyToSpell)
        {
            CancelAction();
        }
        else
            DeSelected();
    }
    
    
}
