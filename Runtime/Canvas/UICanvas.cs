using ProceduralLevel.UnityPlugins.Common.Extended;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public sealed class UICanvas: ExtendedMonoBehaviour
	{
		[SerializeField]
		private Canvas m_Canvas = null;
		[SerializeField]
		private CanvasGroup m_Group = null;

		public int SortingOrder
		{
			get { return m_Canvas.sortingOrder; }
			set { m_Canvas.sortingOrder = value; }
		}

		public void SetAlpha(float alpha)
		{
			m_Group.alpha = alpha;
		}
	}
}
