using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	[CreateAssetMenu(fileName = nameof(ImmediateUIConfig), menuName = UIUnityConsts.MENU_ROOT+nameof(ImmediateUIConfig))]
	public class ImmediateUIConfig : ScriptableObject
	{
		public UIText Label;

		[Header("Toggle")]
		public UIToggle Toggle;
		public UILabelToggle LabelToggle;

		[Header("Button")]
		public UITextButton TextButton;
		public UIIconButton IconButon;
	}
}
