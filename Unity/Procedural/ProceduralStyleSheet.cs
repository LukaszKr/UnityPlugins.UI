using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	[CreateAssetMenu(fileName = nameof(ProceduralStyleSheet), menuName = UIUnityConsts.MENU+nameof(ProceduralStyleSheet))]
	public class ProceduralStyleSheet : ScriptableObject
	{
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
