using ProceduralLevel.Common.Editor;
using ProceduralLevel.Common.Unity;
using ProceduralLevel.UI.Unity;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UI.Samples.Editor
{
	public class SampleLayoutEditorWindow : AExtendedEditorWindow
	{
		public const string TITLE = "Sample Layout Editor";

		public override string Title => TITLE;

		private GridLayoutElement m_GridLayout;
		private LineLayoutElement m_LineLayout;
		private ListLayoutElement m_ListLayout;

		private GridRowEntry m_LineRow;
		private GridRowEntry m_ListRow;

		[MenuItem(UIUnityConsts.SAMPLE_MENU+TITLE)]
		public static void OpenEditorWindow()
		{
			GetWindow<SampleLayoutEditorWindow>();
		}

		protected override void Initialize()
		{
			PrepareLayout();
		}

		private void PrepareLayout()
		{
			m_GridLayout = new GridLayoutElement(6);
			m_GridLayout.StartNewRow();
			m_GridLayout.Rect = new LayoutRect(50, 50, 200, 200);
			m_GridLayout.Add(new LayoutElement(), 3);
			m_GridLayout.Add(new LayoutElement(), 2);

			for(int x = 0; x < 3; ++x)
			{
				m_GridLayout.StartNewRow();
				m_GridLayout.Add(new LayoutElement(), 2);
				m_GridLayout.Add(new LayoutElement(), 1);
				m_GridLayout.Add(new LayoutElement(), 3);
			}


			m_LineLayout = new LineLayoutElement();
			m_LineLayout.Rect = new LayoutRect(50, 400, 400, 25);
			m_LineLayout.AddStatic(new LayoutElement(), 25);
			m_LineLayout.AddFlexible(new LayoutElement(), 1);
			m_LineLayout.AddStatic(new LayoutElement(), 25);
			m_LineLayout.AddFlexible(new LayoutElement(), 1);
			m_LineLayout.AddStatic(new LayoutElement(), 25);

			m_ListLayout = new ListLayoutElement();
			m_ListLayout.Add(new LayoutElement(50, 50));
			m_ListLayout.Add(new LayoutElement(100, 100));
			m_ListLayout.Add(new LayoutElement(70, 70));

			m_LineRow = m_GridLayout.Add(m_LineLayout, 5);
			m_ListRow = m_GridLayout.Add(m_ListLayout, 5);
		}

		protected override void Draw()
		{
			int newCount = EditorGUILayout.IntSlider(m_GridLayout.ColumnCount, 5, 12);
			m_GridLayout.Rect.Width = newCount*50;
			if(GUILayout.Button("Reset"))
			{
				PrepareLayout();
			}

			m_LineRow.Width = newCount;
			m_ListRow.Width = newCount;
			m_GridLayout.SetColumnCount(newCount);
			m_GridLayout.DoLayout();
			Draw(m_GridLayout, 1);
			//Draw(m_LineLayout, 1);
		}

		protected void Draw(ALayoutElement layout, int depth)
		{
			float tint = 1f/depth;
			Color color = new Color(tint, tint, tint);
			Rect rect = layout.Rect.ToUnity();
			EditorGUI.DrawRect(rect, color);
			if(layout is ILayoutGroupElement group)
			{
				Matrix4x4 current = GUI.matrix;
				GUIExt.PushMatrix(current*Matrix4x4.Translate(rect.position));
				foreach(ALayoutElement element in group.GetElements())
				{
					Draw(element, depth+1);
				}
				GUIExt.PopMatrix();
			}
		}
	}
}
