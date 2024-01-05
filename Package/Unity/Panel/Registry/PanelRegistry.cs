using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	[CreateAssetMenu(fileName = nameof(PanelRegistry), menuName = UIUnityConsts.MENU_ROOT+nameof(PanelRegistry))]
	public class PanelRegistry : APanelRegistry
	{
		[SerializeField]
		private APanel[] m_Panels = null;

		public void SetPanels(List<APanel> panels)
		{
			m_Panels = panels.ToArray();
		}

		public override APanel FindPanelPrefab(Type panelType)
		{
			int length = m_Panels.Length;
			for(int x = 0; x < length; ++x)
			{
				APanel panel = m_Panels[x];
				if(panel.GetType() == panelType)
				{
					return panel;
				}
			}
			return null;
		}
	}
}
