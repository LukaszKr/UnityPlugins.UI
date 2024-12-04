using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	[CreateAssetMenu(fileName = NAME, menuName = UIUnityConsts.MENU+NAME)]
	public class ProceduralStyleSheet : ScriptableObject
	{
		public const string NAME = nameof(ProceduralStyleSheet);

		[Header("Buttons")]
		public UIButton Button;
		public UITextButton TextButton;
		public UIIconButton IconButton;

		[Header("Labels")]
		public UILabel Label;

		[Header("Toggles")]
		public UIToggle Toggle;
		public UITextToggle TextToggle;

		[Header("Text Fields")]
		public UITextField TextField;

		[Header("Int Fields")]
		public UIIntField IntField;

		[Header("Float Fields")]
		public UIFloatField FloatField;

		[Header("Layouts")]
		public UILineLayout VerticalLayout;
		public UILineLayout HorizontalLayout;
	}
}
