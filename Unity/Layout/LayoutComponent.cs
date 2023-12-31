using System;
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
		private RectTransform m_RectComponent;

		public RectTransform RectComponent => m_RectComponent;

		#region Context
		private void Setup(Layout layout)
		{
			if(m_Layout != null)
			{
				throw new InvalidOperationException();
			}
			if(m_RectComponent == null)
			{
				m_RectComponent = GetComponent<RectTransform>();
			}
			m_Layout = layout;
			m_Layout.OnChanged.AddListener(OnLayoutChangedHandler);
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

		public TObject Insert<TObject>(TObject target)
			where TObject : Component
		{
			RectTransform rect = target.GetComponent<RectTransform>();
			rect.SetParent(Transform, false);
			rect.anchorMin = new Vector2(0f, 0f);
			rect.anchorMax = new Vector2(1f, 1f);
			rect.anchoredPosition = new Vector2(0f, 0f);
			rect.sizeDelta = default;
			return target;
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

			m_RectComponent.ApplyLayout(m_Layout);
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
			if(m_RectComponent == null)
			{
				m_RectComponent = GetComponent<RectTransform>();
			}
		}
	}
}
