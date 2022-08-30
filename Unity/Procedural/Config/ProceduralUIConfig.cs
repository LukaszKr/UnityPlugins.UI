using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	[CreateAssetMenu(fileName = nameof(ProceduralUIConfig), menuName = UIUnityConsts.MENU_ROOT+nameof(ProceduralUIConfig))]
	public class ProceduralUIConfig : ScriptableObject
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
