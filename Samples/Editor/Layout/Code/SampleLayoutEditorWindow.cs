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

		private LayoutElement m_VerticalLine;

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
			m_VerticalLine = new LayoutElement(ELayoutOrientation.Vertical);
			m_VerticalLine.Rect.Y = 20;
			LayoutElement line = m_VerticalLine.AddStatic(new LayoutElement(), 50);
			line.AddFlexible(new LayoutElement(), 3);
			line.AddFlexible(new LayoutElement(), 2);

			line = m_VerticalLine.AddStatic(new LayoutElement(), 50);
			line.AddFlexible(new LayoutElement(), 2);
			line.AddFlexible(new LayoutElement(), 1);
			line.AddFlexible(new LayoutElement(), 3);

			LayoutElement horizontalLine = new LayoutElement();
			horizontalLine.AddStatic(new LayoutElement(), 25);
			horizontalLine.AddFlexible(new LayoutElement(), 1);
			horizontalLine.AddStatic(new LayoutElement(), 25);
			horizontalLine.AddFlexible(new LayoutElement(), 1);
			horizontalLine.AddStatic(new LayoutElement(), 25);

			LayoutElement verticalLine = new LayoutElement();
			verticalLine.Orientation = ELayoutOrientation.Vertical;
			verticalLine.AddStatic(new LayoutElement(), 50);
			verticalLine.AddStatic(new LayoutElement(), 100);
			verticalLine.AddStatic(new LayoutElement(), 70);

			LayoutElement horizontalLine2 = new LayoutElement();
			horizontalLine2.AddStatic(new LayoutElement(), 150);
			horizontalLine2.AddFlexible(new LayoutElement(), 2);
			horizontalLine2.AddFlexible(new LayoutElement(), 3);

			m_VerticalLine.AddStatic(horizontalLine, 20);
			m_VerticalLine.AddStatic(verticalLine);
			m_VerticalLine.AddStatic(horizontalLine2, 40);
		}

		protected override void Draw()
		{
			m_VerticalLine.Rect.Width = Screen.width;
			m_VerticalLine.Rect.Height = Screen.height;
			m_VerticalLine.DoLayout();
			Draw(m_VerticalLine, 1);
			//Draw(m_LineLayout, 1);
		}

		protected void Draw(LayoutElement element, int depth)
		{
			float tint = 1f/depth;
			Color color = new Color(tint, tint, tint);
			Rect rect = element.Rect.ToUnity();
			EditorGUI.DrawRect(rect, color);

			Matrix4x4 current = GUI.matrix;
			GUIExt.PushMatrix(current*Matrix4x4.Translate(rect.position));
			foreach(LayoutElement child in element.GetChildrens())
			{
				Draw(child, depth+1);
			}
			GUIExt.PopMatrix();
		}
	}
}
