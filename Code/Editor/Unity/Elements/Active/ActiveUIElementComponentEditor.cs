using UnityEditor;
using UnityPlugins.Common.Editor;
using UnityPlugins.UI.Unity;

namespace UnityPlugins.UI.Editor
{
	[CustomEditor(typeof(ActiveUIElementComponent))]
	public class ActiveUIElementComponentEditor : AExtendedEditor<ActiveUIElementComponent>
	{
		protected override void Initialize()
		{
		}

		protected override void Draw()
		{
			EditorGUILayout.LabelField($"{nameof(Target.State)}: {Target.State}");
			DrawDefaultInspector();
		}
	}
}
