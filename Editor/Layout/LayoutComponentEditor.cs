using System.Reflection;
using ProceduralLevel.Common.Editor;
using ProceduralLevel.UI.Unity;
using UnityEditor;

namespace ProceduralLevel.UI.Editor
{
	[CustomEditor(typeof(LayoutComponent))]
	public class LayoutComponentEditor : AExtendedEditor<LayoutComponent>
	{
		protected override void Initialize()
		{
			DrawDefault = true;
		}

		protected override void Draw()
		{
			FieldInfo field = Target.GetType().GetField("m_Layout", BindingFlags.Instance | BindingFlags.NonPublic);
			Layout layout = (Layout)field.GetValue(Target);
			if(layout != null)
			{
				EditorGUI.BeginDisabledGroup(true);

				EditorGUILayout.BeginVertical("box");
				{
					EditorGUILayout.RectField(nameof(layout.Rect), layout.Rect.ToUnity());
					EditorGUILayout.Toggle(nameof(layout.StretchWithChildren), layout.StretchWithChildren);
					EditorGUILayout.FloatField(nameof(layout.Align), layout.Align);
					EditorGUILayout.Toggle(nameof(layout.Active), layout.Active);
				}
				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical("box");
				{
					EditorGUILayout.LabelField("Child Layout", EditorStyles.boldLabel);
					EditorGUILayout.EnumPopup(nameof(layout.Axis), layout.Axis);
					EditorGUILayout.IntField(nameof(layout.ElementsSpacing), layout.ElementsSpacing);
				}
				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical("box");
				{
					EditorGUILayout.LabelField("Element Dimensions", EditorStyles.boldLabel);
					EditorGUILayout.EnumPopup(nameof(layout.ElementType), layout.ElementType);
					EditorGUILayout.IntField(nameof(layout.ElementSize), layout.ElementSize);
					EditorGUILayout.Toggle(nameof(layout.ExpandToParent), layout.ExpandToParent);
				}
				EditorGUILayout.EndVertical();

				EditorGUI.EndDisabledGroup();
			}
		}
	}
}
