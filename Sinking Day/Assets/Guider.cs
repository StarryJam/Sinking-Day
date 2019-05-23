using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using QFramework.EventID;

public class Guider : MonoBehaviour {

    private bool nextStepTrigger = false;
    public List<string> tipTexts;
    public GameObject step1Obj;
    public GameObject step2Obj;
    public Vector2 step3Offset;
    public GameObject step4Obj;
    public GameObject step5Obj;


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
        if (obj.GetComponent<RectTransform>() != null)
            PointAtRect(obj.GetComponent<RectTransform>());
        else
            PointAtGameObj(obj);
    }

    IEnumerator WaitForComplete(int textIndex = -1)
    {
        if (textIndex > 0)
            UIManager.SetPopInfo(tipTexts[textIndex]);
        while (!nextStepTrigger)
        {
            yield return new WaitForEndOfFrame();
        }
        UIManager.HidePopInfo();
    }


    IEnumerator CameraControl()
    {
        UIManager.SetPopInfo(tipTexts[0]);
        while (!(w && a && s && d && up && down))
        {
            yield return new WaitForEndOfFrame();
        }
        UIManager.HidePopInfo();
        StartCoroutine(Step1());
    }

    IEnumerator Step1()//第一步：点击单位
    {
        PointAtObj(step1Obj);
        yield return StartCoroutine(WaitForComplete(1));
        nextStepTrigger = false;
        StartCoroutine(Step2());
    }

    IEnumerator Step2()
    {
        PointAtObj(step2Obj);
        yield return StartCoroutine(WaitForComplete(2));
        nextStepTrigger = false;
        StartCoroutine(Step3());
    }

    IEnumerator Step3()
    {
        PointAtObj(Grid.map.GetNodeFromPosition(new Vector3(step3Offset.x, 0, step3Offset.y)).mapCube.gameObject);
        yield return StartCoroutine(WaitForComplete(3));
        nextStepTrigger = false;
        StartCoroutine(Step4());
    }

    IEnumerator Step4()
    {
        PointAtObj(step4Obj);
        yield return StartCoroutine(WaitForComplete(4));
        nextStepTrigger = false;
        StartCoroutine(Step5());
    }

    IEnumerator Step5()
    {
        PointAtObj(step5Obj);
        yield return StartCoroutine(WaitForComplete(5));
        nextStepTrigger = false;
    }

    private void AddSignal(GameObject obj)
    {
        obj.AddComponent<GuiderSignal>();
    }

    private void NextStep(int key, params object[] param)
    {
        nextStepTrigger = true;
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