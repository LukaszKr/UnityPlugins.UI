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
			UIBuilder builder = new UIBuilder(Transform);
			builder.BeginHorizontal("TopBar");
			
			builder.EndHorizontal();
			builder.BeginHorizontal("Middle");

			builder.EndHorizontal();

			m_Container = LayoutComponent.Create("Container", Transform);
			LayoutComponent topBar = m_Container.Create("TopBar").SetHorizontal().SetStatic(100);
			LayoutComponent middle = m_Container.Create("Middle").SetHorizontal();
			for(int x = 0; x < 3; ++x)
			{
				middle.Create($"{x}", m_LayoutComponentPrefab).SetFlexible(x+1);
			}
			LayoutComponent bottomBar = m_Container.Create("BottomBar").SetHorizontal().SetStatic(100);

			topBar.Create("TopLeft", m_LayoutComponentPrefab).SetStatic(100);
			topBar.Create("TopMiddle");
			topBar.Create("TopRight", m_LayoutComponentPrefab).SetStatic(100);

			bottomBar.Create("BottomLeft", m_LayoutComponentPrefab).SetStatic(100);
			bottomBar.Create("BottomMiddle");
			bottomBar.Create("BottomRight").SetStatic(200).Spawn(m_NestedPrefab);

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
			m_Container.SetRect(new LayoutRect(20, 20, (int)rect.rect.width-40, (int)rect.rect.height-40));
			m_Container.DoLayout();
		}
	}
}
