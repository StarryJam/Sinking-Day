/****************************************************************************
 * 2019.5 DESKTOP-2V6FD5B
 ****************************************************************************/

namespace QFramework.Example
{
	using UnityEngine;
	using UnityEngine.UI;

	public partial class UI_StatementHud
	{
		public const string NAME = "UI_StatementHud";
		[SerializeField] public Slider healthBar;

		protected override void ClearUIComponents()
		{
			healthBar = null;
		}
	}
}
