﻿using ProceduralLevel.Common.Event;
using ProceduralLevel.UI.Unity;
using UnityEngine;

namespace ProceduralLevel.UI.Samples
{
	public class TestPanel : APanel
	{
		[SerializeField]
		private RectTransform m_FramePrefab = null;
		[SerializeField]
		private RectTransform m_NestedPrefab = null;
		[SerializeField]
		private UITextField m_TextField = null;
		[SerializeField]
		private UIIntField m_IntField = null;
		[SerializeField]
		private UIFloatField m_FloatField = null;

		private LayoutComponent m_Container;

		protected override void OnInitialize(EventBinder binder)
		{
			LayoutBuilder builder = new LayoutBuilder();
			m_Container = builder.Begin(Transform);
			builder.BeginGroup("TopBar").SetHorizontal().SetStatic(100);
			builder.Create("TopLeft").SetStatic(100).Spawn(m_FramePrefab);
			builder.Create("TopMiddle");
			builder.Create("TopRight").SetStatic(100).Spawn(m_FramePrefab);
			builder.EndGroup();

			builder.BeginGroup("Middle").SetHorizontal();
			for(int x = 0; x < 3; ++x)
			{
				builder.BeginGroup($"{x}").SetFlexible(x+1).SetExpandToParent(true).Spawn(m_FramePrefab);
				builder.Create("Label").SetStatic(50).SetMargin(20);
				builder.Create($"{x}:Text").SetStatic(40).SetMargin(10, 0).Spawn(m_TextField);
				builder.Create($"{x}:Int").SetStatic(40).SetMargin(10, 0).Spawn(m_IntField);
				builder.Create($"{x}:Float").SetStatic(40).SetMargin(10, 0).Spawn(m_FloatField);
				for(int y = 0; y < 5; ++y)
				{
					builder.Create($"{x}:{y}").SetStatic(40).SetMargin(10, 0).Spawn(m_FramePrefab);
				}
				builder.EndGroup();
			}
			builder.EndGroup();

			builder.BeginGroup("BottomBar").SetHorizontal().SetStatic(100);
			//example of element not occupying full height/width of parent
			builder.Create("BottomLeft").SetStatic(100).SetExpandToParent(false).SetHeight(50).Spawn(m_NestedPrefab);
			builder.Create("BottomMiddle");
			builder.Create("BottomRight").SetStatic(200).Spawn(m_FramePrefab);
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
			m_Container.SetSize((int)rectTransform.rect.width-40, (int)rectTransform.rect.height-40);
			m_Container.SetMargin(20);
			m_Container.DoLayout();
		}
	}
}
