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
			m_VerticalLine = new Layout(ELayoutOrientation.Vertical);
			m_VerticalLine.Rect.Y = 20;
			Layout line = m_VerticalLine.AddStatic(new Layout(), 50);
			line.AddFlexible(new Layout(), 3);
			line.AddFlexible(new Layout(), 2);

			line = m_VerticalLine.AddStatic(new Layout(), 50);
			line.AddFlexible(new Layout(), 2);
			line.AddFlexible(new Layout(), 1);
			line.AddFlexible(new Layout(), 3);

			Layout horizontalLine = new Layout();
			horizontalLine.AddStatic(new Layout(), 25);
			horizontalLine.AddFlexible(new Layout(), 1);
			horizontalLine.AddStatic(new Layout(), 25);
			horizontalLine.AddFlexible(new Layout(), 1);
			horizontalLine.AddStatic(new Layout(), 25);

			Layout verticalLine = new Layout();
			verticalLine.Orientation = ELayoutOrientation.Vertical;
			verticalLine.AddStatic(new Layout(), 50);
			verticalLine.AddStatic(new Layout(), 100);
			verticalLine.AddStatic(new Layout(), 70);

			Layout horizontalLine2 = new Layout();
			horizontalLine2.AddStatic(new Layout(), 150);
			horizontalLine2.AddFlexible(new Layout(), 2);
			horizontalLine2.AddFlexible(new Layout(), 3);

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
