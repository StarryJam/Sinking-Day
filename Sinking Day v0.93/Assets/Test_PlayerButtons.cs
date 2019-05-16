using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_PlayerButtons : MonoBehaviour {

    public Button b1;
    public Button b2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (StageManager.turnStage == StageManager.TurnStage.playMoveing)
        {
            b1.gameObject.SetActive(true);
            b2.gameObject.SetActive(true);
        }
        else
        {
            b1.gameObject.SetActive(false);
            b2.gameObject.SetActive(false);
        }
	}
}
