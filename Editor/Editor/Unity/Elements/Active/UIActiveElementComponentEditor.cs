using UnityEditor;
using UnityPlugins.Common.Editor;
using UnityPlugins.UI.Unity;

namespace UnityPlugins.UI.Editor
{
	[CustomEditor(typeof(UIActiveElementComponent))]
	public class UIActiveElementComponentEditor : AExtendedEditor<UIActiveElementComponent>
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
