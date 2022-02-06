using System;
using System.Collections.Generic;
using ProceduralLevel.UnityPlugins.Common.Unity.Extended;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public abstract class AUIManager : ExtendedMonoBehaviour
	{
		public readonly PanelManager PanelManager = new PanelManager();

		private readonly List<APanel> m_SpawnedPanels = new List<APanel>();

		public virtual void Initialize()
		{
		}

		protected virtual void Update()
		{
			PanelManager.Update();
		}

		public TPanel GetPanel<TPanel>()
			where TPanel : APanel
		{
			TPanel existingPanel = FindPanel<TPanel>();
			if(existingPanel != null)
			{
				return existingPanel;
			}

			TPanel panelPrefab = GetPanelPrefab<TPanel>();
			if(panelPrefab != null)
			{
				UICanvas canvas = Instantiate(GetCanvasPrefab(), Transform, false);
				TPanel spawnedPanel = Instantiate(panelPrefab, canvas.Transform);
				spawnedPanel.Setup(canvas, PanelManager);
				m_SpawnedPanels.Add(spawnedPanel);
				return spawnedPanel;
			}
			throw new NullReferenceException();
		}

		public TPanel FindPanel<TPanel>()
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

			return null;
		}

		protected abstract UICanvas GetCanvasPrefab();
		protected abstract TPanel GetPanelPrefab<TPanel>() where TPanel : APanel;
	}
}
