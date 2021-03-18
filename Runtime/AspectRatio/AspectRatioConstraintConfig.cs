using UnityEngine;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	[CreateAssetMenu(fileName = nameof(AspectRatioConstraintConfig), menuName = UIConsts.MENU_ROOT+nameof(AspectRatioConstraintConfig))]
	public class AspectRatioConstraintConfig: ScriptableObject
	{
		public float MinAspect = 0.5f;
		public float MaxAspect = 2f;
	}
}
