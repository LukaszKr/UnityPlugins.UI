using ProceduralLevel.UnityPlugins.Common.Extended;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public abstract class AUIManager: ExtendedMonoBehaviour
	{
		[SerializeField]
		private CanvasManager m_PanelOrderManager = null;

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

			PanelRegistry registry = GetPanelRegistry();
			TPanel panelPrefab = registry.GetPanel<TPanel>();
			if(panelPrefab != null)
			{
				UICanvas canvas = Instantiate(GetCanvasPrefab(), Transform, false);
				TPanel spawnedPanel = Instantiate(panelPrefab, canvas.Transform);
				spawnedPanel.Setup(canvas, m_PanelOrderManager);
				m_SpawnedPanels.Add(spawnedPanel);
				return spawnedPanel;
			}
			return null;
		}

		protected abstract UICanvas GetCanvasPrefab();
		protected abstract PanelRegistry GetPanelRegistry();
	}
}
