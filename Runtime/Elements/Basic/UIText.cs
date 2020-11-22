using UnityEngine;
using UnityEngine.UI;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public class UIText: APanelElement
	{
		[SerializeField]
		private Text m_Text = null;

		public void SetText(string value)
		{
			m_Text.text = value;
		}
	}
}
