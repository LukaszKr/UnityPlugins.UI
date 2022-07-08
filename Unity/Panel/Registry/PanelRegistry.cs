using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Unity
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

		public override TPanel GetPanelPrefab<TPanel>()
		{
			int length = m_Panels.Length;
			for(int x = 0; x < length; ++x)
			{
				TPanel panel = m_Panels[x] as TPanel;
				if(panel != null)
				{
					return panel;
				}
			}
			return null;
		}
	}
}
