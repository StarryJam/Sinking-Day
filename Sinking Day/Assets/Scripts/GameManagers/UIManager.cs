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

    public Canvas mainCanvas;

    public static Image tipBox;
    public static GameObject tipArrow;

    public static Canvas ui_RangeCursorOnMap;
    public static Canvas ui_RangeCursorOnUnit;

    public static UI_SkillTreePanel skillTreePanel;
    public static UI_SkillTree skillTree;
    public static UI_SkillTreeLayer skillTreeLayer;

    public static Canvas ui_UnitStateHud;
    public static UI_UnitInfoHud ui_UnitInfoHud;
    public static UI_PopInfo ui_PopInfo;
    public static GameObject skillBtn;

    public static SkillPanel skillPanel;

    public static Button endTurnBtn;

    private Vector3 mouseHitPoint;
    private LayerMask mouseHitMask;


    private void Awake()
    {
        uiManager = this;

        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();


        tipBox = ((GameObject)Resources.Load("UI/TipBox")).GetComponent<Image>();
        tipBox = Instantiate(tipBox, mainCanvas.transform);
        tipBox.gameObject.SetActive(false);
        tipArrow = ((GameObject)Resources.Load("UI/TipArrow"));
        tipArrow = Instantiate(tipArrow);
        tipArrow.gameObject.SetActive(false);
        ui_PopInfo = GameObject.FindGameObjectWithTag("UI_PopInfo").GetComponent<UI_PopInfo>();
        ui_PopInfo.gameObject.SetActive(false);
        /*----加载导航UI----*/

        defaultPointer = (Texture2D)Resources.Load("Image/Pointer/Pointer");
        onEnemyPointer = (Texture2D)Resources.Load("Image/Pointer/OnEnemy");
        onFriendPointer = (Texture2D)Resources.Load("Image/Pointer/OnFriend");
        onPointPointer = (Texture2D)Resources.Load("Image/Pointer/OnPoint");
        Cursor.SetCursor(defaultPointer, Vector2.zero, CursorMode.Auto);
        /*----加载光标----*/
        
        ui_RangeCursorOnUnit = ((GameObject)Resources.Load("UI/RangeCursorOnUnit")).GetComponent<Canvas>();
        ui_RangeCursorOnMap = ((GameObject)Resources.Load("UI/RangeCursorOnMap")).GetComponent<Canvas>();
        /*----加载技能指示器----*/


        skillBtn = (GameObject)Resources.Load("Assets/Resources/UI/skillButton");
        /*----加载技能面板UI----*/




        skillTreePanel = ((GameObject)Resources.Load("UI/UI_SkillTree/UI_SkillTreePanel")).GetComponent<UI_SkillTreePanel>();
        skillTree = ((GameObject)Resources.Load("UI/UI_SkillTree/UI_SkillTree")).GetComponent<UI_SkillTree>();
        skillTreeLayer = ((GameObject)Resources.Load("UI/UI_SkillTree/UI_SkillTreeLayer")).GetComponent<UI_SkillTreeLayer>();
        /*----加载技能树UI----*/


        ui_UnitStateHud = ((GameObject)Resources.Load("UI/UI_UnitHuds/UI_UnitStateHud")).GetComponent<Canvas>();
        ui_UnitInfoHud = GameObject.FindGameObjectWithTag("UI_UnitInfohud").GetComponent<UI_UnitInfoHud>();
        ui_UnitInfoHud.gameObject.SetActive(false);
        UnitStateUI.actionPointImg = Resources.Load("Image/UI/UI_Statementhud/ActionPoint_Img", typeof(Sprite)) as Sprite;
        UnitStateUI.usingActionPointImg = Resources.Load("Image/UI/UI_Statementhud/ActionPoint(Using)_Img", typeof(Sprite)) as Sprite;
        /*----加载单位UI----*/


        skillPanel = GameObject.FindGameObjectWithTag("SkillPanel").GetComponent<SkillPanel>();


        //playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        
    }

    private void Start()
    {

        endTurnBtn = GameObject.FindGameObjectWithTag("EndTurnBtn").GetComponent<Button>();
        endTurnBtn.onClick.AddListener(StageManager.stageManager.PlayerEnd);
        /*----初始化回合结束按钮----*/
    }

    public void Update()
    {
        if (PointerEvent.isOnUnit && !PointerEvent.isOnUI)//鼠标在单位上时显示单位信息
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

    public static void HideSelectInformation()
    {
        skillPanel.gameObject.SetActive(false);
        skillTreePanel.gameObject.SetActive(false);
    }

    public static void ShowSelectInformation()
    {
        skillPanel.gameObject.SetActive(true);
    }



    public static void ShowInformation()
    {
        ui_UnitInfoHud.gameObject.SetActive(true);
        ui_UnitInfoHud.unit = PointerEvent.pointerOnObj.GetComponent<Unit>();
        ui_UnitInfoHud.transform.position = Input.mousePosition;

    }

    public static void HideInformation()
    {
        ui_UnitInfoHud.gameObject.SetActive(false);

    }

    private static void ResetInformation()
    {
    //    information.RemoveRange(0, information.Count);
    //}

    //public static void NotEnoughMoney()
    //{
    //    money.SetTrigger("NotEnoughMoney");
    }

    public static void ShowTipBox(RectTransform rect)
    {
        tipBox.gameObject.SetActive(true);
        tipBox.rectTransform.SetSizeWidth(rect.rect.width * 1.2f);
        tipBox.rectTransform.SetSizeHeight(rect.rect.height * 1.2f);
        tipBox.transform.position = rect.position;
    }
    public static void HideTipBox()
    {
        tipBox.gameObject.SetActive(false);
    }

    public static void ShowTipArrow(GameObject obj)
    {
        tipArrow.gameObject.SetActive(true);
        tipArrow.GetComponent<TipArrow>().SetPosition(obj.transform.position);
    }
    public static void HideTipArror()
    {
        tipArrow.gameObject.SetActive(false);
    }

    public static void SetPopInfo(string text)
    {
        ui_PopInfo.gameObject.SetActive(true);
        ui_PopInfo.SetText(text);
    }

    public static void HidePopInfo()
    {
        ui_PopInfo.gameObject.SetActive(false);
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