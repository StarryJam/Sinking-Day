using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    static public IEnumerator MoveToTarget(GameObject obj, Vector3 targetPos, float speed)
    {
        float distance = Vector3.Distance(obj.transform.position, targetPos);
        float time = distance / speed;
        float scheduleSpeed = 1 / time;
        float schedule = 0;//动画插值
        
        while (schedule < 1)
        {
            schedule += Time.deltaTime * scheduleSpeed;
            if (schedule > 1)
                schedule = 1;

            //Debug.Log(schedule);
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPos, schedule);
            yield return new WaitForEndOfFrame();
        }
    }
}
