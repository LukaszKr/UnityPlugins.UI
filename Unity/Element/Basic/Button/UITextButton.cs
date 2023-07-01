﻿using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class UITextButton : UIButton
	{
		[SerializeField]
		private UIText m_Text = null;

		public UIText Text => m_Text;
	}
}
