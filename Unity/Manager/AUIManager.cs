using System;
using System.Collections.Generic;
using ProceduralLevel.UnityPlugins.Common.Unity.Extended;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public abstract class AUIManager : ExtendedMonoBehaviour
	{
		[SerializeField]
		private PanelManager m_PanelManager = null;

		private readonly List<APanel> m_SpawnedPanels = new List<APanel>();

		public void Initialize()
		{
			m_PanelManager.Initialize();
		}

		public TPanel GetPanel<TPanel>()
			where TPanel : APanel
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
			throw new NullReferenceException();
		}

		protected abstract UICanvas GetCanvasPrefab();
		protected abstract TPanel GetPanelPrefab<TPanel>() where TPanel : APanel;
	}
}
