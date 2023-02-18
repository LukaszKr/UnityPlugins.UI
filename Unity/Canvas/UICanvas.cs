using ProceduralLevel.UnityPlugins.Common.Unity.Extended;
using UnityEngine;
using UnityEngine.UI;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public sealed class UICanvas : ExtendedMonoBehaviour
	{
		[SerializeField]
		private Canvas m_Canvas = null;
		[SerializeField]
		private CanvasGroup m_Group = null;
		[SerializeField]
		private GraphicRaycaster m_Raycaster = null;

		public Canvas UnityCanvas => m_Canvas;
		public CanvasGroup CanvasGroup => m_Group;
		public GraphicRaycaster Raycaster => m_Raycaster;

		public int SortingOrder
		{
			get { return m_Canvas.sortingOrder; }
			set { m_Canvas.sortingOrder = value; }
		}
	}
}
