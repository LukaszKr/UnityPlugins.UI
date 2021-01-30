using System.Collections.Generic;
using ProceduralLevel.UnityPlugins.Common.Extended;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public abstract class AUIManager: ExtendedMonoBehaviour
	{
		[SerializeField]
		private PanelManager m_PanelManager = null;

		private readonly List<AUIPanel> m_SpawnedPanels = new List<AUIPanel>();

		public TPanel GetPanel<TPanel>()
			where TPanel : AUIPanel
		{
			int count = m_SpawnedPanels.Count;
			for(int x = 0; x < count; ++x)
			{
				TPanel panel = m_SpawnedPanels[x] as TPanel;
				if(panel != null && panel.GetType() == typeof(TPanel))
				{
					return panel;
				}
			}

			TPanel panelPrefab = GetPanelPrefab<TPanel>();
			if(panelPrefab != null)
			{
				UICanvas canvas = Instantiate(GetCanvasPrefab(), Transform, false);
				TPanel spawnedPanel = Instantiate(panelPrefab, canvas.Transform);
				spawnedPanel.Setup(canvas, m_PanelManager);
				m_SpawnedPanels.Add(spawnedPanel);
				return spawnedPanel;
			}
			return null;
		}

		protected abstract UICanvas GetCanvasPrefab();
		protected abstract TPanel GetPanelPrefab<TPanel>() where TPanel : AUIPanel;
	}
}
