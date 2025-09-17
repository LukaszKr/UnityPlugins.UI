using UnityEditor;
using UnityPlugins.Common.Editor;
using UnityPlugins.Common.Logic;
using UnityPlugins.UI.Unity;

namespace UnityPlugins.UI.Editor
{
	[CustomEditor(typeof(UICanvasScaleControllerComponent))]
	public class UICanvasScaleControllerComponentEditor : AExtendedEditor<UICanvasScaleControllerComponent>
	{
		protected override void Initialize()
		{
		}
		protected override void Draw()
		{
			DrawDefaultInspector();

			Observable<int> scale = UICanvasScaleControllerComponent.Scale;
			scale.Value = EditorGUILayout.IntSlider(nameof(UICanvasScaleControllerComponent.Scale), scale.Value, 10, 300);
		}
	}
}