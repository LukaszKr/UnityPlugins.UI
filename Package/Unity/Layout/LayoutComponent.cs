﻿using System;
using ProceduralLevel.Common.Unity.Extended;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	[RequireComponent(typeof(RectTransform))]
	public partial class LayoutComponent : ExtendedMonoBehaviour
	{
		protected Layout m_Layout;
		private LayoutRect m_DisplayedRect;

		[SerializeField]
		private RectTransform m_RectTransform;

		public Layout Layout => m_Layout;
		public RectTransform RectTransform => m_RectTransform;

		#region Context
		private void Setup(Layout layout)
		{
			if(m_Layout != null)
			{
				throw new InvalidOperationException();
			}
			if(m_RectTransform == null)
			{
				if(!TryGetComponent(out m_RectTransform))
				{
					m_RectTransform = GameObject.AddComponent<RectTransform>();
				}
			}
			m_Layout = layout;
			m_Layout.Active = GameObject.activeSelf;
			m_Layout.OnChanged.AddListener(OnLayoutChangedHandler);
		}

		protected virtual void OnEnable()
		{
			if(m_Layout != null)
			{
				m_Layout.Active = true;
			}
		}

		protected virtual void OnDisable()
		{
			if(m_Layout != null)
			{
				m_Layout.Active = false;
			}
		}

		private void OnDestroy()
		{
			m_Layout.Destroy();
		}
		#endregion

		#region Nesting
		public TPrefab Spawn<TPrefab>(TPrefab prefab)
			where TPrefab : Component
		{
			TPrefab spawned = Instantiate(prefab);
			return Insert(spawned);
		}

		public TComponent Insert<TComponent>(TComponent component)
			where TComponent : Component
		{
			RectTransform rect = component.GetComponent<RectTransform>();
			rect.SetParent(Transform, false);
			rect.anchorMin = new Vector2(0f, 0f);
			rect.anchorMax = new Vector2(1f, 1f);
			rect.anchoredPosition = new Vector2(0f, 0f);
			rect.sizeDelta = default;
			return component;
		}
		#endregion

		#region Create
		public static LayoutComponent Create(string name, Transform parent)
		{
			Layout layout = new Layout(null);
			return Create(name, parent, layout);
		}

		private static LayoutComponent Create(string name, Transform parent, Layout layout)
		{
			GameObject go = new GameObject(name);
			go.transform.SetParent(parent, false);
			LayoutComponent component = go.AddComponent<LayoutComponent>();
			component.Setup(layout);
			return component;
		}

		public LayoutComponent Create(string name)
		{
			return Create(name, Transform, m_Layout.CreateChild());
		}
		#endregion

		#region Update
		public void DoLayout()
		{
			m_Layout.DoLayout();
		}

		private void UpdateRect()
		{
			if(m_Layout.Rect == m_DisplayedRect)
			{
				return;
			}

			m_RectTransform.ApplyLayout(m_Layout);
			m_DisplayedRect = m_Layout.Rect;
		}
		#endregion


		#region Callbacks
		private void OnLayoutChangedHandler()
		{
			UpdateRect();
		}
		#endregion

		private void OnValidate()
		{
			if(m_RectTransform == null)
			{
				m_RectTransform = GetComponent<RectTransform>();
			}
		}
	}
}
