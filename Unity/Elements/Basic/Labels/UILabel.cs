using UnityPlugins.Common.Logic;
using TMPro;
using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	public class UILabel : AUIElement
	{
		[SerializeField]
		private TextMeshProUGUI m_Text = null;

		#region Element
		protected override void OnInitialize(EventBinder binder)
		{
		}
		#endregion

		public UILabel SetText(string text)
		{
			m_Text.text = text;
			return this;
		}
	}
}
