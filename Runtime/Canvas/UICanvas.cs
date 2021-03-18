using ProceduralLevel.UnityPlugins.Common.Extended;
using UnityEngine;
using UnityEngine.UI;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public sealed class UICanvas: ExtendedMonoBehaviour
	{
		[SerializeField]
		private Canvas m_Canvas = null;
		[SerializeField]
		private CanvasGroup m_Group = null;
		[SerializeField]
		private GraphicRaycaster m_Raycaster = null;

		public int SortingOrder
		{
			get { return m_Canvas.sortingOrder; }
			set { m_Canvas.sortingOrder = value; }
		}

		public GraphicRaycaster Raycaster { get { return m_Raycaster; } }

		public void SetAlpha(float alpha)
		{
			m_Group.alpha = alpha;
		}
	}
}
