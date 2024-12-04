using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	[CreateAssetMenu(fileName = NAME, menuName = UIUnityConsts.MENU+NAME)]
	public class AspectRatioConstraintConfig : ScriptableObject
	{
		public const string NAME = nameof(AspectRatioConstraintConfig);

		public float MinAspect = 0.5f;
		public float MaxAspect = 2f;
	}
}
