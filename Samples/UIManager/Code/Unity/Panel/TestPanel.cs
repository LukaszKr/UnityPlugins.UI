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
		[SerializeField]
		private BasicUIBuilderConfig m_UIBuilderConfig = null;

		private LayoutComponent m_Container;

		protected override void OnInitialize(EventBinder binder)
		{
			BasicUIBuilder builder = new BasicUIBuilder(m_UIBuilderConfig);
			m_Container = builder.Begin(Transform);
			builder.BeginGroup("TopBar").SetHorizontal().SetStatic(100);
			builder.Create("TopLeft", m_LayoutComponentPrefab).SetStatic(100);
			builder.Create("TopMiddle");
			builder.Create("TopRight", m_LayoutComponentPrefab).SetStatic(100);
			builder.EndGroup();

			builder.BeginGroup("Middle").SetHorizontal();
			for(int x = 0; x < 3; ++x)
			{
				builder.BeginGroup($"{x}", m_LayoutComponentPrefab).SetFlexible(x+1).SetExpandToParent(true);
				builder.Label("Label", $"Column {x}");
				for(int y = 0; y < 10; ++y)
				{
					builder.Create($"{x}:{y}", m_LayoutComponentPrefab).SetStatic(40);
				}
				builder.EndGroup();
			}
			builder.EndGroup();

			builder.BeginGroup("BottomBar").SetHorizontal().SetStatic(100);
			//example of element not occupying full height/width of parent
			builder.Create("BottomLeft", m_LayoutComponentPrefab).SetStatic(100).SetExpandToParent(false).SetHeight(50);
			builder.Create("BottomMiddle");
			builder.Create("BottomRight").SetStatic(200).Spawn(m_NestedPrefab);
			builder.EndGroup();
			
			builder.End();

			FitToScreen();
		}

		public new void Show()
		{
			base.Show();
		}

		private void Update()
		{
			FitToScreen();
		}

		private void FitToScreen()
		{
			RectTransform rectTransform = GetComponent<RectTransform>();
			m_Container.SetRect(new LayoutRect(20, 20, (int)rectTransform.rect.width-40, (int)rectTransform.rect.height-40));
			m_Container.DoLayout();
		}
	}
}
