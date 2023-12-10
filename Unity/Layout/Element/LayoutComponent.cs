using System;
using ProceduralLevel.Common.Unity.Extended;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	[RequireComponent(typeof(RectTransform))]
	public class LayoutComponent : ExtendedMonoBehaviour
	{
		private Layout m_Layout;
		private LayoutRect m_DisplayedRect;

		[SerializeField]
		private RectTransform m_RectComponent;

		public Layout Layout => m_Layout;
		public RectTransform RectComponent => m_RectComponent;

		#region Context
		public void Setup(Layout layout)
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

		#region Create
		public static LayoutComponent Create(Layout parent, Transform parentTransform, string name, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
		{
			return LayoutFactory.Create(new Layout(parent, orientation, ELayoutEntryType.Flexible, 1), parentTransform, name);
		}

		public LayoutComponent AddFlexible(string name, int value, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
		{
			return AddFlexible(name, value, null, orientation);
		}

		public TPrefab AddFlexible<TPrefab>(string name, int value, TPrefab prefab, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
			where TPrefab : Component
		{
			return AddFlexible(name, value, null, orientation).Spawn(prefab);
		}

		public LayoutComponent AddFlexible(string name, int value, LayoutComponent prefab, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
		{
			Layout layout = m_Layout.AddFlexible(value, orientation);
			return LayoutFactory.Create(layout, Transform, name, prefab);
		}

		public LayoutComponent AddStatic(string name, int value, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
		{
			return AddStatic(name, value, null, orientation);
		}

		public LayoutInstancePair<TPrefab> AddStatic<TPrefab>(string name, int value, TPrefab prefab, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
			where TPrefab : Component
		{
			LayoutComponent layout = AddStatic(name, value, null, orientation);
			TPrefab instance = layout.Spawn(prefab);
			return new LayoutInstancePair<TPrefab>(layout, instance);
		}

		public LayoutComponent AddStatic(string name, int value, LayoutComponent prefab, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
		{
			Layout layout = m_Layout.AddStatic(value, orientation);
			return LayoutFactory.Create(layout, Transform, name, prefab);
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
		public void UpdateRect()
		{
			if(m_Layout.Rect == m_DisplayedRect)
			{
				return;
			}

			m_RectComponent.ApplyLayout(m_Layout);
			m_DisplayedRect = m_Layout.Rect;
		}
		#endregion

		public LayoutComponent SetAlign(float align)
		{
			m_Layout.Align = align;
			return this;
		}

		public LayoutComponent SetActive(bool active)
		{
			m_Layout.Active = active;
			return this;
		}

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
