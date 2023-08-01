using ProceduralLevel.Common.Event;
using ProceduralLevel.UI.Unity;
using UnityEngine;

namespace ProceduralLevel.UI.Samples
{
	public class TestPanel : APanel
	{
		[SerializeField]
		private LayoutComponent m_LayoutComponentPrefab = null;
		[SerializeField]
		private RectTransform m_NestedPrefab = null;

		private LayoutElement m_Container;

		protected override void OnInitialize(EventBinder binder)
		{
			m_Container = new LayoutElement();
			m_Container.Orientation = ELayoutOrientation.Vertical;

			LayoutComponent container = LayoutComponent.Create(m_Container, Transform, "Container");
			LayoutComponent topBar = container.AddStatic("TopBar", 100);
			LayoutComponent middle = container.AddFlexible("Middle", 1);
			middle.Element.Orientation = ELayoutOrientation.Vertical;
			for(int x = 0; x < 3; ++x)
			{
				middle.AddFlexible($"{x}", x+1, m_LayoutComponentPrefab);
			}
			LayoutComponent bottomBar = container.AddStatic("BottomBar", 100);

			topBar.AddStatic("TopLeft", 100, m_LayoutComponentPrefab);
			topBar.Element.AddFlexible();
			topBar.AddStatic("TopRight", 100, m_LayoutComponentPrefab);

			bottomBar.AddStatic("BottomLeft", 100, m_LayoutComponentPrefab);
			bottomBar.Element.AddFlexible();
			bottomBar.AddStatic("BottomRight", 200).SpawnAndNest(m_NestedPrefab);
		}

		public new void Show()
		{
			base.Show();
		}

		private void Update()
		{
			RectTransform rect = GetComponent<RectTransform>();
			m_Container.Rect = new LayoutRect(20, 20, (int)rect.rect.width-40, (int)rect.rect.height-40);
			m_Container.DoLayout();
		}
	}
}
