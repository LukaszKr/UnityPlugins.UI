using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public class UIManager : AUIManager
	{
		[SerializeField]
		private UICanvas m_Canvas = null;

		[SerializeField]
		private APanelRegistry[] m_Registries = null;

		protected override UICanvas GetCanvasPrefab()
		{
			return m_Canvas;
		}

		protected override TPanel GetPanelPrefab<TPanel>()
		{
			int length = m_Registries.Length;
			for(int x = 0; x < length; ++x)
			{
				APanelRegistry registry = m_Registries[x];
				TPanel panelPrefab = registry.GetPanelPrefab<TPanel>();
				if(panelPrefab)
				{
					return panelPrefab;
				}
			}
			return null;
		}
	}
}