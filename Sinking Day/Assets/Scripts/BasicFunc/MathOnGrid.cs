using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathOnGrid {

    public static float DistanceBetween(GameObject obj1, GameObject obj2)
    {
        return Vector3.Distance(obj1.transform.position, obj2.transform.position) / 2;
    }

	
}
