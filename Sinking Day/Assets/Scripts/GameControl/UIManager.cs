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

    public static Canvas ui_RangeCursorOnMap;
    public static Canvas ui_RangeCursorOnUnit;

    public static Canvas ui_UnitStateHud;
    public static UI_UnitInfoHud ui_UnitInfoHud;

    public static SkillPanel skillPanel;

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
        
        ui_RangeCursorOnUnit = ((GameObject)Resources.Load("UI/RangeCursorOnUnit")).GetComponent<Canvas>();
        ui_RangeCursorOnMap = ((GameObject)Resources.Load("UI/RangeCursorOnMap")).GetComponent<Canvas>();
        /*----加载技能指示器----*/


        ui_UnitStateHud = ((GameObject)Resources.Load("UI/UI_UnitHuds/UI_UnitStateHud")).GetComponent<Canvas>();
        ui_UnitInfoHud = GameObject.FindGameObjectWithTag("UI_UnitInfohud").GetComponent<UI_UnitInfoHud>();
        ui_UnitInfoHud.gameObject.SetActive(false);
        UnitStateUI.actionPointImg = Resources.Load("Image/UI/UI_Statementhud/ActionPoint_Img", typeof(Sprite)) as Sprite;
        UnitStateUI.usingActionPointImg = Resources.Load("Image/UI/UI_Statementhud/ActionPoint(Using)_Img", typeof(Sprite)) as Sprite;
        /*----加载单位UI----*/


        skillPanel = GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>();
        //UIMgr.OpenPanel<UI_endTurnButtonPanel>(prefabName: "Resources/UI/UI_endTurnButtonPanel");
        /*----初始化UI----*/

        //playerState = GameObject.Find("Player").GetComponent<PlayerState>();


    }

    public void Update()
    {
        if (PointerEvent.isOnUnit)//鼠标在单位上时显示单位信息
        {
            ShowInformation();
        }
        else
        {
            HideInformation();
        }
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
    
    //更新选中的单位信息（由鼠标点击事件调用）
    public static void UpdateSelectInformation()
    {
        skillPanel.UpdatePanel();
    }

    

    public static void ShowInformation()
    {
        ui_UnitInfoHud.gameObject.SetActive(true);
        ui_UnitInfoHud.unit = PointerEvent.pointerOnObj.GetComponent<Unit>();
        ui_UnitInfoHud.transform.position = Input.mousePosition;

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
        ui_UnitInfoHud.gameObject.SetActive(false);

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