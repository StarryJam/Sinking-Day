/****************************************************************************
 * 2019.5 DESKTOP-2V6FD5B
 ****************************************************************************/

namespace QFramework.Example
{
	using UnityEngine;
	using UnityEngine.UI;

	public partial class UI_endTurnButtonPanel
	{
		public const string NAME = "UI_endTurnButtonPanel";
		[SerializeField] public Button endTurnButton;

		protected override void ClearUIComponents()
		{
			endTurnButton = null;
		}
	}
}
