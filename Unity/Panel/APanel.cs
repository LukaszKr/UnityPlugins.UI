﻿using System;

namespace ProceduralLevel.UnityPlugins.UI.Unity
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
				ShowAnimation();
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
				HideAnimation();
				m_Manager.Remove(this);
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		protected virtual void ShowAnimation()
		{
			m_Canvas.GameObject.SetActive(true);
		}

		protected virtual void HideAnimation()
		{
			m_Canvas.GameObject.SetActive(false);
		}

		protected virtual void OnShow()
		{
		}

		protected virtual void OnHide()
		{
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
