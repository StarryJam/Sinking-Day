using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PopInfo : MonoBehaviour {

    public Text text;

    public void SetText(string _text)
    {
        text.text = _text;
    }
}
