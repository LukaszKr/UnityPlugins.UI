using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	[CreateAssetMenu(fileName = NAME, menuName = UIUnityConsts.MENU_ROOT+NAME)]
	public class BasicUIBuilderConfig : ScriptableObject
	{
		private const string NAME = nameof(BasicUIBuilderConfig);

		public UILabel Label;
	}
}
