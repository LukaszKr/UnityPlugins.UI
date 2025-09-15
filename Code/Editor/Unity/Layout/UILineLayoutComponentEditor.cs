using System;
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
			DrawExpandOptions();
		}

		private void DrawExpandOptions()
		{
			SerializedProperty expandMain = serializedObject.FindProperty("m_ExpandMainAxis");
			SerializedProperty expandOther = serializedObject.FindProperty("m_ExpandOtherAxis");
			string mainLabel = string.Empty;
			string otherLabel = string.Empty;

			switch(Target.Axis)
			{
				case ELayoutAxis.Vertical:
					mainLabel = "Expand Vertical";
					otherLabel = "Expand Horizontal";
					break;
				case ELayoutAxis.Horizontal:
					mainLabel = "Expand Horizontal";
					otherLabel = "Expand Vertical";
					break;
			}

			EditorGUILayout.PropertyField(expandMain, new GUIContent(mainLabel));
			EditorGUILayout.PropertyField(expandOther, new GUIContent(otherLabel));
		}
	}
}
