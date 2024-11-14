using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	public class UITextToggle : UIToggle
	{
		[SerializeField]
		private UILabel m_Label = null;

		public UILabel Label => m_Label;
	}
}
