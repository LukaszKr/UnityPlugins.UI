using UnityEditor;
using UnityEngine;
using UnityPlugins.Common.Editor;
using UnityPlugins.UI.Unity;

namespace UnityPlugins.UI.Editor
{
	[CustomEditor(typeof(UILineLayout))]
	public class UILineLayoutEditor : AExtendedEditor<UILineLayout>
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
