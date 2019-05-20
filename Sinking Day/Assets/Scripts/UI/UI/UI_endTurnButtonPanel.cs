/****************************************************************************
 * 2019.5 DESKTOP-2V6FD5B
 ****************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using QFramework.EventID;

namespace QFramework.Example
{
	public class UI_endTurnButtonPanelData : UIPanelData
	{
		// TODO: Query Mgr's Data
	}

	public partial class UI_endTurnButtonPanel : UIPanel
	{
		protected override void InitUI(IUIData uiData = null)
		{
			mData = uiData as UI_endTurnButtonPanelData ?? new UI_endTurnButtonPanelData();
			//please add init code here
		}

		protected override void ProcessMsg (int eventId,QMsg msg)
		{
			throw new System.NotImplementedException ();
		}

		protected override void RegisterUIEvent()
		{
            //endTurnButton.onClick.AddListener(() =>
            //{
            //    QEventSystem.SendEvent(GameEventID.Skill.endTurn);
            //});
		}

		protected override void OnShow()
		{
			base.OnShow();
		}

		protected override void OnHide()
		{
			base.OnHide();
		}

		protected override void OnClose()
		{
			base.OnClose();
		}

		void ShowLog(string content)
		{
			Debug.Log("[ UI_endTurnButtonPanel:]" + content);
		}

		UI_endTurnButtonPanelData mData = null;
	}
}