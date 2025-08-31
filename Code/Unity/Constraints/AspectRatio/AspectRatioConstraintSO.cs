using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	[CreateAssetMenu(fileName = NAME, menuName = UIUnityConsts.MENU + NAME)]
	public class AspectRatioConstraintSO : ScriptableObject
	{
		public const string NAME = nameof(AspectRatioConstraintSO);

		public float MinAspect = 0.5f;
		public float MaxAspect = 2f;
	}
}
