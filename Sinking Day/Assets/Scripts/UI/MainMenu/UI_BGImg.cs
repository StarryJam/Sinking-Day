using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BGImg : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Rect rect = GetComponent<RectTransform>().rect;
        float height = rect.height;
        float width = rect.width;
        float proportion = width / height;
        rect.height = Screen.height;
        rect.width = height * proportion;
	}
	
}
