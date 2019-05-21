using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using QFramework.EventID;

public class MonoBasedOnTurn : MonoBehaviour {

	// Use this for initialization
	public void Awake () {
        QEventSystem.RegisterEvent(GameEventID.Turn.endTurn, TurnStartTrigger);
    }
	
	private void TurnStartTrigger(int key, params object[] param)
    {
        AtTurnStart();
    }

    public virtual void AtTurnStart()
    {
    }
}
