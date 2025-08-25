﻿using System;
using UnityPlugins.Common.Logic;
using UnityPlugins.Common.Unity;

namespace UnityPlugins.UI.Unity
{
	public abstract class APanelComponent : ExtendedMonoBehaviour
	{
		private UICanvasComponent m_Canvas;
		private PanelsManager m_Manager;
		private readonly EventBinder m_ElementBinder = new EventBinder();

		private bool m_IsShown = false;

		public bool IsShown => m_IsShown;
		public UICanvasComponent Canvas => m_Canvas;

		internal void Setup(UICanvasComponent canvas, PanelsManager manager)
		{
			m_Canvas = canvas;
			canvas.name = $"Canvas - {name}";
			m_Manager = manager;
			OnInitialize(m_ElementBinder);
		}

		protected abstract void OnInitialize(EventBinder binder);

		protected void Show()
		{
			if(CanShow())
			{
				m_IsShown = true;
				m_ElementBinder.Enable();
				OnShow();
				m_Manager.Add(this, m_Canvas);
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
				m_ElementBinder.Disable();
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

		public void BringForward()
		{
			Canvas.SortingOrder = m_Manager.GetNextSortOrder();
		}
	}
}
