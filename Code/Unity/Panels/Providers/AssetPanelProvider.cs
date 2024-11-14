using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	[CreateAssetMenu(fileName = nameof(AssetPanelProvider), menuName = UIUnityConsts.MENU+nameof(AssetPanelProvider))]
	public class AssetPanelProvider : APanelProvider
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
