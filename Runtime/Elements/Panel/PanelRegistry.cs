using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	[CreateAssetMenu(fileName = nameof(PanelRegistry), menuName = UIConsts.MENU_ROOT+nameof(PanelRegistry))]
	public class PanelRegistry: ScriptableObject
	{
		[SerializeField]
		private AUIPanel[] m_Panels = null;

		public void SetPanels(List<AUIPanel> panels)
		{
			m_Panels = panels.ToArray();
		}

		public TPanel GetPanel<TPanel>()
			where TPanel: AUIPanel
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
