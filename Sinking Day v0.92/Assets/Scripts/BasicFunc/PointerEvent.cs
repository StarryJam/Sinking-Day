using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cakeslice;

public class PointerEvent : MonoBehaviour {

    public static GameObject selected;
    //public List<string> selectableObj;
    public static bool isCasting = false;
    public static Skill castingSkill;
    public static RaycastHit hit;
    public static pointerState _pointerState;

    public static bool isOnMap = false;
    public static bool isOnUI = false;
    public static bool isOnUnit = false;
    public static bool canSelecte = false;

    public static GameObject pointerOnObj;

    public enum pointerState
    {
        normal,
        choosingTarget
    }

    public static void ChangingPointerState(pointerState state)
    {
        _pointerState = state;
        if (_pointerState == pointerState.normal)
        {
            Cursor.SetCursor(UIManager.defaultPointer, Vector2.zero, CursorMode.Auto);
        }
        if (_pointerState == pointerState.choosingTarget)
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
                isOnUnit = true;
            else
                isOnUnit = false;

            //判断能否选择
            if (pointerOnObj.GetComponent<Selected>() != null)
                canSelecte = true;
            else
                canSelecte = false;
        }
    }

    public void DeSelect()
    {
        StartCoroutine(DeSelectAtTheEndOfFrame());
    }

    private IEnumerator DeSelectAtTheEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        selected.GetComponent<Selected>().DeSelected();
        selected = null;
    }



    // Update is called once per frame
    void Update () {
        PointerOnWhat();
        //Debug.Log(pointerOnObj);

        if (Input.GetMouseButtonUp(0))//鼠标左键松开
        {
            if (!isOnUI)//判断是否点击在UI上
            {
                if (isCasting)//判断是否正在释放技能
                {
                    castingSkill.CastSkill();
                }
                else
                {
                    if (pointerOnObj != selected && (hit.collider.GetComponent<SubObject>() == null || hit.collider.GetComponent<SubObject>().FindFather() != selected)) //判断是否点击当前选择对象或其父对象
                    {
                        if (canSelecte)
                        {
                            //取消当前选择
                            if (selected != null)
                            {
                                DeSelect();
                            }
                            //选中点击物体
                            selected = hit.collider.gameObject;
                            selected.GetComponent<Selected>().BeSelected();
                        }
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(1))//鼠标右键点击
        {
            if (selected != null)
            {
                //若玩家正在操作单位则不取消选择
                if (selected.GetComponent<UnitOfPlyer>() != null && selected.GetComponent<UnitOfPlyer>().state != Unit.UnitState.holding)
                {
                    Debug.Log("yep");
                }
                else
                {
                    if(selected.GetComponent<UnitOfPlyer>() != null)
                    {
                        Debug.Log(selected.GetComponent<UnitOfPlyer>().state);
                    }
                    DeSelect();
                }
            }
            if (isCasting)
            {
                castingSkill.StopCast();
            }
        }
    }
}
