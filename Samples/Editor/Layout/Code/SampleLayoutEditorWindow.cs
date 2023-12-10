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

		private Layout m_Root;

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
			m_Root = new Layout(null, ELayoutOrientation.Vertical);
			m_Root.Rect.Y = 20;
			Layout line = m_Root.AddStatic(50, ELayoutOrientation.Horizontal);
			line.AddFlexible(3);
			line.AddFlexible(2);

			line = m_Root.AddStatic(50, ELayoutOrientation.Horizontal);
			line.AddFlexible(2);
			line.AddFlexible(1);
			//inactive element is not included in calculations
			Layout flex3 = line.AddFlexible(3);
			flex3.Active = false;

			line = m_Root.AddStatic(50, ELayoutOrientation.Horizontal);
			line.Align = 0.5f;
			line.AddStatic(100);

			Layout horizontalLine = m_Root.AddStatic(20, ELayoutOrientation.Horizontal);
			horizontalLine.AddStatic(25);
			horizontalLine.AddFlexible(1);
			horizontalLine.AddStatic(25);
			horizontalLine.AddFlexible(1);
			horizontalLine.AddStatic(25);

			Layout verticalLine = m_Root.AddStatic(0, ELayoutOrientation.Vertical);
			verticalLine.AddStatic(50);
			verticalLine.AddStatic(100);
			verticalLine.AddStatic(70);

			Layout horizontalLine2 = m_Root.AddStatic(40, ELayoutOrientation.Horizontal);
			horizontalLine2.AddStatic(150);
			horizontalLine2.AddFlexible(2);
			horizontalLine2.AddFlexible(3);

			DoLayout();
		}

		protected override void Draw()
		{
			EditorGUILayout.BeginHorizontal();
			if(GUILayout.Button("Layout"))
			{
				DoLayout();
			}
			if(GUILayout.Button("RESET"))
			{
				PrepareLayout();
			}
			EditorGUILayout.EndHorizontal();
			Draw(m_Root, 1);
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

		private void DoLayout()
		{
			m_Root.Rect.Width = Screen.width;
			m_Root.Rect.Height = Screen.height;
			m_Root.DoLayout();
		}
	}
}
