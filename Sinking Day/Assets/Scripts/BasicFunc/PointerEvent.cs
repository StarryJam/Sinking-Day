using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cakeslice;

public class PointerEvent : MonoBehaviour {

    public static GameObject selected;
    //public List<string> selectableObj;
    public static bool isCasting = false;
    //public static Skill castingSkill;
    public static RaycastHit hit;
    public static PointerState _pointerState;

    public static bool isOnMap = false;
    public static bool isOnUI = false;
    public static bool isOnUnit = false;
    public static bool canSelecte = false;
    public static bool isOnEnemy = false;
    public static bool isOnFriend = false;

    public static GameObject pointerOnObj;

    public enum PointerState
    {
        normal,
        choosingTarget
    }

    public static void ChangingPointerState(PointerState state)
    {
        _pointerState = state;
        if (_pointerState == PointerState.normal)
        {
            Cursor.SetCursor(UIManager.defaultPointer, Vector2.zero, CursorMode.Auto);
        }
        if (_pointerState == PointerState.choosingTarget)
        {
            Cursor.SetCursor(UIManager.onPointPointer, new Vector2(100, 100), CursorMode.Auto);
        }
    }

    private void PointerOnWhat()
    {
        
        if (EventSystem.current.IsPointerOverGameObject() == true)//判断鼠标是否在UI上
            isOnUI = true;
        else
            isOnUI = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isCollider = Physics.Raycast(ray, out hit, 1000);
        if (isCollider)
        {
            pointerOnObj = hit.collider.gameObject;

            //判断鼠标是否在地图上
            if (pointerOnObj.GetComponent<Map_BaseCube>() != null)
                isOnMap = true;
            else
                isOnMap = false;

            //判断是否在单位上
            if (pointerOnObj.GetComponent<Unit>() != null)
            {
                isOnUnit = true;
                if (pointerOnObj.GetComponent<Unit>().camp == Unit.Camp.enemy)
                {
                    isOnEnemy = true;
                }
            }
            else
                isOnUnit = false;

            //判断能否选择
            if (pointerOnObj.GetComponent<Selectee>() != null)
                canSelecte = true;
            else
                canSelecte = false;
        }
        else
        {
            pointerOnObj = null;
            isOnMap = false;
            isOnUI = false;
            isOnUnit = false;
            canSelecte = false;
            isOnEnemy = false;
            isOnFriend = false;
        }
    }

    public void Select()
    {
        if (pointerOnObj != null)
        {
            if(pointerOnObj.GetComponent<Selectee>() != null)
            {
                if (selected == null)
                {
                    pointerOnObj.GetComponent<Selectee>().BeSelected();
                }
                else
                {
                    //点击不相同目标的时候重新选择
                    if (selected != pointerOnObj)
                    {
                        selected.GetComponent<Selectee>().DeSelected();
                        Select();
                    }
                }
                if (selected.GetComponent<UnitOfPlyer>() != null)
                {
                    UIManager.UpdateSelectInformation();
                }
            }
        }
    }
    

    public void Cancel()
    {
        if (selected.GetComponent<UnitOfPlyer>() != null)
        {
            selected.GetComponent<UnitOfPlyer>().Cancel();
        }
        else
            selected.GetComponent<Selectee>().DeSelected();
        //StartCoroutine(DeSelectAtTheEndOfFrame());
    }

    private IEnumerator DeSelectAtTheEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        selected.GetComponent<Selectee>().DeSelected();
        selected = null;
    }


    
    void Update () {
        PointerOnWhat();

        if (Input.GetMouseButtonUp(0))//鼠标左键松开
        {
            //Debug.Log(pointerOnObj);
            if (!isOnUI)//判断是否点击在UI上
            {
                if (_pointerState == PointerState.choosingTarget)//判断是否正在释放技能
                {
                    //castingSkill.CastSkill();
                    if (selected.GetComponent<UnitOfPlyer>() != null)
                    {
                        selected.GetComponent<UnitOfPlyer>().DoAction();
                    }
                }
                else
                {
                    Select();
                }
            }
        }
        else if (Input.GetMouseButtonUp(1))//鼠标右键松开
        {
            if (selected != null)
            {
                //若玩家正在操作单位则不取消选择
                    Cancel();
            }
            if (isCasting)
            {
                //castingSkill.StopCast();
            }
        }
    }
}
