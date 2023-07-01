using System;

namespace ProceduralLevel.UI.Unity
{
	public abstract class APanel : AUIElement
	{
		private UICanvas m_Canvas;
		private PanelManager m_Manager;

		private bool m_IsShown = false;

		public bool IsShown => m_IsShown;
		public UICanvas Canvas => m_Canvas;
		public PanelManager Manager => m_Manager;

		internal void Setup(UICanvas canvas, PanelManager manager)
		{
			m_Canvas = canvas;
			m_Manager = manager;

			TryInitialize();
		}

		protected void Show()
		{
			if(CanShow())
			{
				m_IsShown = true;
				m_Manager.Add(this, m_Canvas);
				OnShow();
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		public void Hide()
		{
			if(CanHide())
			{
				m_IsShown = false;
				OnHide();
				m_Manager.Remove(this);
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		protected virtual void OnShow()
		{
			m_Canvas.GameObject.SetActive(true);
		}

		protected virtual void OnHide()
		{
			m_Canvas.GameObject.SetActive(false);
		}

		protected virtual bool CanShow()
		{
			return !m_IsShown;
		}

		protected virtual bool CanHide()
		{
			return m_IsShown;
		}
	}
}
