using ProceduralLevel.Common.Event;
using UnityEngine;
using TMPro;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public class UIText : APanelElement
	{
		[SerializeField]
		private TextMeshProUGUI m_Text = null;

		#region Element
		protected override void OnInitialize(EventBinder binder)
		{
		}
		#endregion

		public void SetText(string text)
		{
			m_Text.text = text;
		}
	}
}
