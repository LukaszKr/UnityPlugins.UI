using UnityEngine;
using UnityEngine.UI;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public class PanelText: PanelElement
	{
		[SerializeField]
		private Text m_Text = null;

		public Text UnityText { get { return m_Text; } }

		public void SetText(string value)
		{
			m_Text.text = value;
		}
	}
}
