﻿using ProceduralLevel.Common.Unity.Extended;
using UnityEngine;
using UnityEngine.UI;

namespace ProceduralLevel.UI.Unity
{
	public sealed class UICanvas : ExtendedMonoBehaviour
	{
		[SerializeField]
		private Canvas m_Canvas = null;
		[SerializeField]
		private CanvasGroup m_Group = null;

		public Canvas UnityCanvas => m_Canvas;
		public CanvasGroup CanvasGroup => m_Group;

		public int SortingOrder
		{
			get { return m_Canvas.sortingOrder; }
			set { m_Canvas.sortingOrder = value; }
		}
	}
}
