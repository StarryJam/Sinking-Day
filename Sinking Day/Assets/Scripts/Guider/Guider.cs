using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using QFramework.EventID;

public class Guider : MonoBehaviour {

    [System.Serializable]
    public class GuideStep
    {
        public string tipText;
        public GameObject obj;
        public float delay;
    }

    private bool nextStepTrigger = false;
    public List<GuideStep> steps;


    bool w = false;
    bool a = false;
    bool s = false;
    bool d = false;
    bool up = false;
    bool down = false;
    /*-----镜头控制任务判定-----*/

    // Use this for initialization
    void Start () {
        QEventSystem.RegisterEvent(GameEventID.Guider.compeletStep, NextStep);
        StartCoroutine(CameraControl());
	}
	
	// Update is called once per frame
	void Update () {
        if(!(w && a && s && d && up && down))
        {
            if (Input.GetKeyDown(KeyCode.W))
                w = true;
            if (Input.GetKeyDown(KeyCode.A))
                a = true;
            if (Input.GetKeyDown(KeyCode.S))
                s = true;
            if (Input.GetKeyDown(KeyCode.D))
                d = true;
            if (Input.GetKeyDown(KeyCode.W))
                w = true;
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                up = true;
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                down = true;
        }
        
    }

    private void PointAtRect(RectTransform obj){
        UIManager.ShowTipBox(obj);
        AddSignal(obj.gameObject);
    }

    private void PointAtGameObj(GameObject obj)
    {
        UIManager.ShowTipArrow(obj);
        AddSignal(obj.gameObject);
    }

    private void PointAtObj(GameObject obj)
    {
        if (obj.GetComponent<GuiderOnMap>() != null)//判断是否指向地图
        {
            PointAtObj(Grid.map.GetNodeFromPosition(new Vector3(obj.transform.position.x, 0, obj.transform.position.y)).mapCube.gameObject);
            Destroy(obj);
        }
        else
        {
            if (obj.GetComponent<RectTransform>() != null)
                PointAtRect(obj.GetComponent<RectTransform>());
            else
                PointAtGameObj(obj);
        }

    }
    

    IEnumerator WaitForComplete(string text)
    {
        UIManager.SetPopInfo(text);
        while (!nextStepTrigger)
        {
            yield return new WaitForEndOfFrame();
        }
        UIManager.HidePopInfo();
    }


    IEnumerator CameraControl()
    {
        UIManager.SetPopInfo("使用<color=red>WASD</color>和<color=red>滚轮</color>来控制镜头");
        while (!(w && a && s && d && up && down))
        {
            yield return new WaitForEndOfFrame();
        }
        UIManager.HidePopInfo();
        StartCoroutine(Guide());
    }

    IEnumerator Guide()//第一步：点击单位
    {
        foreach(var step in steps)
        {
            PointAtObj(step.obj);
            yield return StartCoroutine(WaitForComplete(step.tipText));
            yield return new WaitForSeconds(step.delay);
            nextStepTrigger = false;
        }
    }



    private void AddSignal(GameObject obj)
    {
        obj.AddComponent<GuiderSignal>();
    }

    private void NextStep(int key, params object[] param)
    {
        nextStepTrigger = true;
    }

    [SerializeField]
    public class GuideObj
    {
        public bool isMap = false;
        public GameObject obj;
        public Vector2 offSet;
    }
}

public class GuiderSignal : MonoBehaviour
{
    Button btn;
    private void Start()
    {
        if (gameObject.GetComponent<Button>())
        {
            btn = gameObject.GetComponent<Button>();
            btn.onClick.AddListener(CompeletStep);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && PointerEvent.hit.collider.gameObject == gameObject)
        {
            CompeletStep();
        }
    }

    private void CompeletStep()
    {
        QEventSystem.SendEvent(GameEventID.Guider.compeletStep);
        UIManager.HideTipArror();
        UIManager.HideTipBox();
        if (gameObject.GetComponent<Button>())
            btn.onClick.RemoveListener(CompeletStep);
        Destroy(this);
    }
}