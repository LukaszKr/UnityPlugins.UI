using UnityEditor;
using UnityEngine;
using UnityPlugins.Common.Editor;
using UnityPlugins.UI.Unity;

namespace UnityPlugins.UI.Editor
{
	[CustomEditor(typeof(UILineLayoutComponent))]
	public class UILineLayoutComponentEditor : AExtendedEditor<UILineLayoutComponent>
	{
		protected override void Initialize()
		{
		}

		protected override void Draw()
		{
			EditorGUILayout.BeginHorizontal("helpbox");
			{
				if(GUILayout.Button("Do Layout"))
				{
					Target.Editor_DoLayout();
				}
			}
			EditorGUILayout.EndHorizontal();
			DrawDefaultInspector();
		}
	}
}
