using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class UILabelToggle : UIToggle
	{
		[SerializeField]
		private UILabel m_Label = null;

		public UILabel Label => m_Label;
	}
}
