/****************************************************************************
 * 2019.5 DESKTOP-2V6FD5B
 ****************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class UI_StatementHudData : UIPanelData
	{
		// TODO: Query Mgr's Data
	}

	public partial class UI_StatementHud : UIPanel
	{
		protected override void InitUI(IUIData uiData = null)
		{
			mData = uiData as UI_StatementHudData ?? new UI_StatementHudData();
			//please add init code here
		}

		protected override void ProcessMsg (int eventId,QMsg msg)
		{
			throw new System.NotImplementedException ();
		}

		protected override void RegisterUIEvent()
		{
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
			Debug.Log("[ UI_StatementHud:]" + content);
		}

		UI_StatementHudData mData = null;
	}
}