using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using QFramework.EventID;

public class MonoBasedOnTurn : MonoBehaviour {

	// Use this for initialization
	public void Awake () {
        QEventSystem.RegisterEvent(GameEventID.TurnManager.turnStart, TurnStartTrigger);
        QEventSystem.RegisterEvent(GameEventID.TurnManager.playerEnd, PlayerEndTrigger);
    }
	
	private void TurnStartTrigger(int key, params object[] param)
    {
        AtTurnStart();
    }

    private void PlayerEndTrigger(int key, params object[] param)
    {
        AtPlayerEnd();
    }

    public virtual void AtTurnStart()
    {
    }

    public virtual void AtPlayerEnd()
    {
    }
}
