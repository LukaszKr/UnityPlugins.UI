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
			Layout layout = Target.Layout;
			if(layout != null)
			{
				EditorGUI.BeginDisabledGroup(true);

				EditorGUILayout.BeginVertical("box");
				{
					EditorGUILayout.RectField(nameof(layout.Rect), layout.Rect.ToUnity());
					EditorGUILayout.LabelField($"{nameof(layout.Margin)}: {layout.Margin}");
					EditorGUILayout.LabelField($"{nameof(layout.Size)}: {layout.Size}");
				}
				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical("box");
				{
					EditorGUILayout.LabelField("Properties", EditorStyles.boldLabel);
					EditorGUILayout.Toggle(nameof(layout.FitToChildren), layout.FitToChildren);
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
					EditorGUILayout.EnumPopup(nameof(layout.LayoutMode), layout.LayoutMode);
					EditorGUILayout.IntField(nameof(layout.LayoutModeSize), layout.LayoutModeSize);
					EditorGUILayout.Toggle(nameof(layout.ExpandToParent), layout.ExpandToParent);
				}
				EditorGUILayout.EndVertical();

				EditorGUI.EndDisabledGroup();
			}
		}
	}
}
