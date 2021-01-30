using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public abstract class AUIPanel: AUIElement
	{
		private UICanvas m_Canvas;
		private APanelManager m_PanelManager;

		private readonly List<APanelElement> m_Elements = new List<APanelElement>();

		private bool m_IsShown = false;

		public IReadOnlyList<APanelElement> Elements { get { return m_Elements; } }

		internal void Setup(UICanvas canvas, APanelManager panelManager)
		{
			m_Canvas = canvas;
			m_PanelManager = panelManager;
		}

		protected override void OnCleanup()
		{
			int count = m_Elements.Count;
			for(int x = 0; x < count; ++x)
			{
				APanelElement element = m_Elements[x];
				OnRemoveElement(element);
			}
			m_Elements.Clear();
		}

		public void Show()
		{
			if(!m_IsShown && CanShow())
			{
				m_IsShown = true;
				m_Canvas.GameObject.SetActive(true);
				m_PanelManager.Add(this, m_Canvas);
				OnShow();
			}
		}

		public void Hide()
		{
			if(m_IsShown)
			{
				m_IsShown = false;
				OnHide();
				m_Canvas.GameObject.SetActive(false);
				m_PanelManager.Remove(this);
			}
		}

		protected virtual bool CanShow()
		{
			return true;
		}

		protected virtual void OnShow()
		{
		}

		protected virtual void OnHide()
		{
		}

		public void AddElement(APanelElement element)
		{
#if UNITY_EDITOR
			if(m_Elements.Contains(element))
			{
				throw new System.ArgumentException();
			}
#endif
			m_Elements.Add(element);
			OnAddElement(element);
		}

		public bool RemoveElement(APanelElement element)
		{
			if(m_Elements.Remove(element))
			{
				OnRemoveElement(element);
				return true;
			}
			return false;
		}

		protected virtual void OnAddElement(APanelElement element) { }
		protected virtual void OnRemoveElement(APanelElement element) { }

		#region Input
		public int GetElementsAt(Vector2 position, APanelElement[] buffer)
		{
			int offset = 0;
			int count = m_Elements.Count;
			for(int x = 0; x < count; ++x)
			{
				APanelElement element = m_Elements[x];
				RectTransform rectTransform = element.RectTransform;
				bool contains = RectTransformUtility.RectangleContainsScreenPoint(rectTransform, position);
				if(contains)
				{
					buffer[offset++] = element;
				}
			}
			return offset;
		}
		#endregion
	}
}
