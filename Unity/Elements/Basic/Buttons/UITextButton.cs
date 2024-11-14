using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	public class UITextButton : UIButton
	{
		[SerializeField]
		private UILabel m_Text = null;

		public UILabel Text => m_Text;
	}
}
