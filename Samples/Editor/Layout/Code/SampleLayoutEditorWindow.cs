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

		private Layout m_VerticalLine;

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
			m_VerticalLine = new Layout();
			m_VerticalLine.Rect.Y = 20;
			Layout line = m_VerticalLine.AddStatic(50, ELayoutOrientation.Horizontal);
			line.AddFlexible(3);
			line.AddFlexible(2);

			line = m_VerticalLine.AddStatic(50, ELayoutOrientation.Horizontal);
			line.AddFlexible(2);
			line.AddFlexible(1);
			line.AddFlexible(3);

			Layout horizontalLine = new Layout(ELayoutOrientation.Horizontal);
			horizontalLine.AddStatic(25);
			horizontalLine.AddFlexible(1);
			horizontalLine.AddStatic(25);
			horizontalLine.AddFlexible(1);
			horizontalLine.AddStatic(25);

			Layout verticalLine = new Layout();
			verticalLine.Orientation = ELayoutOrientation.Vertical;
			verticalLine.AddStatic(50);
			verticalLine.AddStatic(100);
			verticalLine.AddStatic(70);

			Layout horizontalLine2 = new Layout(ELayoutOrientation.Horizontal);
			horizontalLine2.AddStatic(150);
			horizontalLine2.AddFlexible(2);
			horizontalLine2.AddFlexible(3);

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

		protected void Draw(Layout element, int depth)
		{
			float tint = 1f/depth;
			Color color = new Color(tint, tint, tint);
			Rect rect = element.Rect.ToUnity();
			EditorGUI.DrawRect(rect, color);

			Matrix4x4 current = GUI.matrix;
			GUIExt.PushMatrix(current*Matrix4x4.Translate(rect.position));
			foreach(Layout child in element.GetChildrens())
			{
				Draw(child, depth+1);
			}
			GUIExt.PopMatrix();
		}
	}
}
