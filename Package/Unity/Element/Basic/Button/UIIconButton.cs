using UnityEngine;
using UnityEngine.UI;

namespace ProceduralLevel.UI.Unity
{
	public class UIIconButton : UIButton
	{
		[SerializeField]
		private Image m_Icon = null;

		public Image Icon => m_Icon;
	}
}
