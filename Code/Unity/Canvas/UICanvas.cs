using UnityPlugins.Common.Unity;
using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	public sealed class UICanvas : ExtendedMonoBehaviour
	{
		[SerializeField]
		private Canvas m_Canvas = null;
		[SerializeField]
		private CanvasGroup m_CanvasGroup = null;

		public Canvas UnityCanvas => m_Canvas;
		public CanvasGroup CanvasGroup => m_CanvasGroup;

		public int SortingOrder
		{
			get { return m_Canvas.sortingOrder; }
			set { m_Canvas.sortingOrder = value; }
		}
	}
}
