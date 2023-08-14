using ProceduralLevel.Common.Event;
using ProceduralLevel.Common.Unity;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	[RequireComponent(typeof(RectTransform))]
	public class LayoutComponent : AContextComponent<Layout>
	{
		private LayoutRect m_DisplayedRect;

		[SerializeField]
		private RectTransform m_RectComponent;

		public Layout Layout => m_Context;

		#region Context
		protected override void OnInitialize()
		{
			if(m_RectComponent == null)
			{
				m_RectComponent = GetComponent<RectTransform>();
			}
		}

		protected override void OnAttach(EventBinder binder)
		{
		}

		protected override void OnDetach()
		{
			m_DisplayedRect = default;
		}
		#endregion

		#region Create
		public static LayoutComponent Create(Transform parent, string name, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
		{
			return LayoutFactory.Create(new Layout(orientation), parent, name);
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
			Layout layout = m_Context.AddFlexible(value, orientation);
			return LayoutFactory.Create(layout, Transform, name, prefab);
		}

		public LayoutComponent AddStatic(string name, int value, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
		{
			return AddStatic(name, value, null, orientation);
		}

		public TPrefab AddStatic<TPrefab>(string name, int value, TPrefab prefab, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
			where TPrefab : Component
		{
			return AddStatic(name, value, null, orientation).Spawn(prefab);
		}

		public LayoutComponent AddStatic(string name, int value, LayoutComponent prefab, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
		{
			Layout layout = m_Context.AddStatic(value, orientation);
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
		private void Update()
		{
			UpdateRect();
		}

		public void UpdateRect()
		{
			if(m_Context == null || m_Context.Rect == m_DisplayedRect)
			{
				return;
			}

			m_RectComponent.ApplyLayout(m_Context);
			m_DisplayedRect = m_Context.Rect;
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
