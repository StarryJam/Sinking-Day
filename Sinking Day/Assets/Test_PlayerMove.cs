//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;

//public class Test_PlayerMove : MonoBehaviour {

//    public GameObject map;
//    public BaseCube targetCube;
//    public float speed = 10;
//    public static RaycastHit hit;

//    bool isMoving = false;

//    // Use this for initialization
//    void Start() {
//        map = GameObject.FindGameObjectWithTag("Map");
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (!EventSystem.current.IsPointerOverGameObject())//判断是否点击在UI上
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//            bool isCollider = Physics.Raycast(ray, out hit, 1000);

//            if (isCollider)
//            {
//                if (hit.collider.gameObject.GetComponent<Map_BaseCube>() != null) //判断是否在地图上
//                {
//                    map.GetComponent<Grid>().FindPath(transform.position, hit.collider.transform.position);
//                }
//            }
//        }
//        if (Input.GetMouseButtonDown(0) && !isMoving)
//        {
//            StartCoroutine(MovePath(map.GetComponent<Grid>().path));
//            //StartCoroutine(MoveAStep(hit.collider.transform.position));
//            //Debug.Log("121212");
//        }
//    }

//    IEnumerator MovePath(List<Node> path)
//    {
//        isMoving = true;
//        if (path.Count > 0)
//        {
//            for (int i = 0; i < path.Count; i++)
//            {
//                yield return StartCoroutine(MoveAStep(path[i]._worldPos));
//            }
//        }
//        isMoving = false;
//    }

//    IEnumerator MoveAStep(Vector3 toPositon)
//    {
//        isMoving = true;
//        float schedule = 0;//动画插值
//        Vector3 from = transform.position;
//        Vector3 to = toPositon;
//        float time = Vector3.Distance(from, to) / speed;
//        float scheduleSpeed = 1 / time;
//        while (schedule < 1)
//        {
//            schedule += Time.deltaTime*scheduleSpeed;
//            if (schedule > 1)
//                schedule = 1;

//            transform.position = Vector3.Lerp(from, to, schedule);
//            yield return new WaitForEndOfFrame();
//        }
//        isMoving = false;
//    }
    
    
//}
