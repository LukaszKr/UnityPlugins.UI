using ProceduralLevel.Common.Event;
using ProceduralLevel.Common.Unity;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	[RequireComponent(typeof(RectTransform))]
	public class LayoutComponent : AContextComponent<LayoutElement>
	{
		private LayoutRect m_DisplayedRect;

		[SerializeField]
		private RectTransform m_RectComponent;

		public LayoutElement Element => m_Context;

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
		public static LayoutComponent Create(LayoutElement element, Transform parent, string name)
		{
			return LayoutFactory.Create(element, parent, name);
		}

		private LayoutComponent Create(LayoutElement element, string name, LayoutComponent prefab = null)
		{
			return LayoutFactory.Create(element, Transform, name, prefab);
		}

		public LayoutComponent AddFlexible(LayoutElement element, string name, int value = 1, LayoutComponent prefab = null)
		{
			m_Context.AddFlexible(element, value);
			return Create(element, name, prefab);
		}

		public LayoutComponent AddFlexible(string name, int value = 1, LayoutComponent prefab = null)
		{
			return AddFlexible(new LayoutElement(), name, value, prefab);
		}

		public LayoutComponent AddStatic(LayoutElement element, string name, int value, LayoutComponent prefab = null)
		{
			m_Context.AddStatic(element, value);
			return Create(element, name, prefab);
		}

		public LayoutComponent AddStatic(LayoutElement element, string name, LayoutComponent prefab = null)
		{
			m_Context.AddStatic(element, 0);
			return Create(element, name, prefab);
		}

		public LayoutComponent AddStatic(string name, int value, LayoutComponent prefab = null)
		{
			LayoutElement element = m_Context.AddStatic(value);
			return Create(element, name, prefab);
		}
		#endregion

		#region Nesting
		public TObject SpawnAndNest<TObject>(TObject prefab)
			where TObject : Component
		{
			TObject spawned = Instantiate(prefab);
			return Nest(spawned);
		}

		public TObject Nest<TObject>(TObject target)
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
