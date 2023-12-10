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

		private LayoutComponent m_Container;

		protected override void OnInitialize(EventBinder binder)
		{
			m_Container = LayoutComponent.Create(null, Transform, "Container");
			LayoutComponent topBar = m_Container.AddStatic("TopBar", 100, ELayoutOrientation.Horizontal);
			LayoutComponent middle = m_Container.AddFlexible("Middle", 1, ELayoutOrientation.Vertical);
			for(int x = 0; x < 3; ++x)
			{
				middle.AddFlexible($"{x}", x+1, m_LayoutComponentPrefab);
			}
			LayoutComponent bottomBar = m_Container.AddStatic("BottomBar", 100, ELayoutOrientation.Horizontal);

			topBar.AddStatic("TopLeft", 100, m_LayoutComponentPrefab);
			topBar.Layout.AddFlexible();
			topBar.AddStatic("TopRight", 100, m_LayoutComponentPrefab);

			bottomBar.AddStatic("BottomLeft", 100, m_LayoutComponentPrefab);
			bottomBar.Layout.AddFlexible();
			bottomBar.AddStatic("BottomRight", 200, m_NestedPrefab);

			FitToScreen();
		}

		public new void Show()
		{
			base.Show();
		}

		[ContextMenu(nameof(FitToScreen))]
		private void FitToScreen()
		{
			RectTransform rect = GetComponent<RectTransform>();
			m_Container.Layout.Rect = new LayoutRect(20, 20, (int)rect.rect.width-40, (int)rect.rect.height-40);
			m_Container.Layout.DoLayout();
		}
	}
}
