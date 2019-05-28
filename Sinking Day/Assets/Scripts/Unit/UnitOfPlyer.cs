using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using QFramework.EventID;

public class UnitOfPlyer : Unit, Selectee {

    private int needAP;
    [HideInInspector] public RangeCursor rangeCursorPos;

    new public void Awake()
    {
        base.Awake();
        gameObject.tag = "UnitOfPlayer";
        QEventSystem.RegisterEvent(GameEventID.Unit.enemyDie, GainExp);
    }

    new void Start() {
        base.Start();

        rangeCursorPos = transform.Find("RangeCursorPos").GetComponent<RangeCursor>();
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
        animator.SetTrigger("Aim");
    }

    public void ReadyToSpell(Skill skill)
    {
        currentSkill = skill;
        PointerEvent.ChangingPointerState(PointerEvent.PointerState.choosingTarget);
        ChangeState(UnitState.readyToSpell);
        CheakRange(skill.spellRange);
        animator.SetTrigger("Aim");
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

    new public void RemainHolding()
    {
        base.RemainHolding();
        ReadyToUseAP(0);
        PointerEvent.ChangingPointerState(PointerEvent.PointerState.normal);
        rangeCursorPos.Cancel();
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
            RemainHolding();
        }
        else if (state == UnitState.readyToAttack && PointerEvent.isOnEnemy)
        {
            if (rangeCursorPos.unitsInRange.Contains(PointerEvent.pointerOnObj))
            {
                StartCoroutine(Attack(PointerEvent.pointerOnObj.GetComponent<Unit>()));
                RemainHolding();
                UsingAP();
            }
        }
        else if (state == UnitState.readyToSpell && PointerEvent.isOnUnit)
        {
            if (rangeCursorPos.unitsInRange.Contains(PointerEvent.pointerOnObj))
            {
                StartCoroutine(SpellSkill());
                RemainHolding();
                UsingAP();
            }
        }
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
            if(state == UnitState.readyToAttack || state == UnitState.readyToSpell)
            {
                if (PointerEvent.isOnUnit)
                {
                    StartCoroutine(Turning(PointerEvent.pointerOnObj.transform.position));
                }
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
        rangeCursorPos.CheckRange(range);
    }

    public void Cancel()
    {
        if (state == UnitState.readyToAttack || state == UnitState.readyToMove || state == UnitState.readyToSpell)
        {
            CancelAction();
            animator.SetTrigger("Hold");
        }
        else
            DeSelected();
    }

    private void GainExp(int key, params object[] param)
    {
        exp += (int)param[0];
        if (exp >= expNeedForLUP)
        {
            exp -= expNeedForLUP;
            LevelUp();
        }
    }

}
