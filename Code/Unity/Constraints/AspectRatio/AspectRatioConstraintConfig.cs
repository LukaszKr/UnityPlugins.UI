﻿using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	[CreateAssetMenu(fileName = nameof(AspectRatioConstraintConfig), menuName = UIUnityConsts.MENU+nameof(AspectRatioConstraintConfig))]
	public class AspectRatioConstraintConfig : ScriptableObject
	{
		public float MinAspect = 0.5f;
		public float MaxAspect = 2f;
	}
}
