using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using QFramework.Example;

public class UIManager : MonoBehaviour {

    public static UIManager uiManager;

    public static List<string> information = new List<string>();
    //public static PlayerState playerState;
    public static Texture2D defaultPointer;
    public static Texture2D onEnemyPointer;
    public static Texture2D onFriendPointer;
    public static Texture2D onPointPointer;

    public static Canvas UI_RangeCursorOnMap;
    public static Canvas UI_RangeCursorOnUnit;

    public static Canvas UI_UnitStateHud;

    private Vector3 mouseHitPoint;
    private LayerMask mouseHitMask;


    private void Awake()
    {
        uiManager = this;

        defaultPointer = (Texture2D)Resources.Load("Image/Pointer/Pointer");
        onEnemyPointer = (Texture2D)Resources.Load("Image/Pointer/OnEnemy");
        onFriendPointer = (Texture2D)Resources.Load("Image/Pointer/OnFriend");
        onPointPointer = (Texture2D)Resources.Load("Image/Pointer/OnPoint");
        Cursor.SetCursor(defaultPointer, Vector2.zero, CursorMode.Auto);
        /*----加载光标----*/
        
        UI_RangeCursorOnUnit = ((GameObject)Resources.Load("UI/RangeCursorOnUnit")).GetComponent<Canvas>();
        UI_RangeCursorOnMap = ((GameObject)Resources.Load("UI/RangeCursorOnMap")).GetComponent<Canvas>();
        /*----加载技能指示器----*/


        UI_UnitStateHud = ((GameObject)Resources.Load("UI/UI_UnitStateHud")).GetComponent<Canvas>();
        /*----加载单位UI----*/



        //UIMgr.OpenPanel<UI_endTurnButtonPanel>(prefabName: "Resources/UI/UI_endTurnButtonPanel");
        /*----初始化UI----*/

        //playerState = GameObject.Find("Player").GetComponent<PlayerState>();


    }


    //在目标上显示指定UI
    public static Canvas CreateUI(Canvas UI, Transform target)
    {
        Canvas thisUI = Instantiate(UI, target);
        return thisUI.GetComponent<Canvas>();
    }

    public static void DisplayUI(Canvas UI)
    {
        UI.gameObject.SetActive(true);
    }

    public static void HideUI(Canvas UI)
    {
        UI.gameObject.SetActive(false);
    }

    public static void DeleteUI(Canvas targetUI)
    {
        Destroy(targetUI.gameObject);
    }

    //在指定的层上显示跟随鼠标的UI
    public static void UIFollowMouseOnTargetMask(Canvas UI, string layerMask)
    {
         Instantiate(UI.gameObject);
    }
    


    

    public static void ShowInformation()
    {
    //    for (int i = 0; i < informationUI.transform.childCount; i++)
    //    {
    //        if (i < information.Count)
    //            informationUI.transform.GetChild(i).GetComponent<Text>().text = information[i];
    //        else
    //            informationUI.transform.GetChild(i).GetComponent<Text>().text = "";
    //    }
    //    informationUI.GetComponent<Animator>().ResetTrigger("Hide");
    //    informationUI.GetComponent<Animator>().SetTrigger("Show");
    //    ResetInformation();
    }

    public static void HideInformation()
    {
    //    if (informationUI != null)
    //    {
    //        informationUI.GetComponent<Animator>().ResetTrigger("Show");
    //        informationUI.GetComponent<Animator>().SetTrigger("Hide");
    //    }
    }

    private static void ResetInformation()
    {
    //    information.RemoveRange(0, information.Count);
    //}

    //public static void NotEnoughMoney()
    //{
    //    money.SetTrigger("NotEnoughMoney");
    }
}

public class UIFollowMouseOnTargetMask : MonoBehaviour
{
    private string targetMaskName;
    private RaycastHit hit;

    UIFollowMouseOnTargetMask(string targetMask)
    {
        targetMaskName = targetMask;
    }

    private void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask(targetMaskName));
        gameObject.transform.position = hit.point;
    }
}