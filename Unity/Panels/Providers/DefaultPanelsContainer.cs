using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	[CreateAssetMenu(fileName = NAME, menuName = UIUnityConsts.MENU + NAME)]
	public class DefaultPanelsContainer : APanelsContainer
	{
		public const string NAME = nameof(DefaultPanelsContainer);

		[SerializeField]
		private APanelComponent[] m_Panels = null;

		public void SetPanels(List<APanelComponent> panels)
		{
			m_Panels = panels.ToArray();
		}

		public override APanelComponent FindPanelPrefab(Type panelType)
		{
			int length = m_Panels.Length;
			for(int x = 0; x < length; ++x)
			{
				APanelComponent panel = m_Panels[x];
				if(panel.GetType() == panelType)
				{
					return panel;
				}
			}
			return null;
		}
	}
}
