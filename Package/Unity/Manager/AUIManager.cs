using System;
using System.Collections.Generic;
using ProceduralLevel.Common.Unity.Extended;

namespace ProceduralLevel.UI.Unity
{
	public abstract class AUIManager : ExtendedMonoBehaviour
	{
		public readonly PanelManager PanelManager = new PanelManager();

		private readonly List<APanel> m_SpawnedPanels = new List<APanel>();

		public virtual void Initialize()
		{
		}

		public TPanel GetPanel<TPanel>()
			where TPanel : APanel
		{
			return (TPanel)GetPanel(typeof(TPanel));
		}

		public APanel GetPanel(Type panelType)
		{
			APanel existingPanel = FindPanel(panelType);
			if(existingPanel != null)
			{
				return existingPanel;
			}

			APanel panelPrefab = GetPanelPrefab(panelType);
			if(panelPrefab != null)
			{
				UICanvas canvas = Instantiate(GetCanvasPrefab(), Transform, false);
				APanel spawnedPanel = Instantiate(panelPrefab, canvas.Transform);
				spawnedPanel.Setup(canvas, PanelManager);
				m_SpawnedPanels.Add(spawnedPanel);
				return spawnedPanel;
			}
			throw new NullReferenceException();
		}

		public TPanel FindPanel<TPanel>()
			where TPanel : APanel
		{
			return FindPanel(typeof(TPanel)) as TPanel;
		}

		public APanel FindPanel(Type panelType)
		{
			int count = m_SpawnedPanels.Count;
			for(int x = 0; x < count; ++x)
			{
				APanel panel = m_SpawnedPanels[x];
				if(panel.GetType() == panelType)
				{
					return panel;
				}
			}

			return null;
		}

		protected TPanel GetPanelPrefab<TPanel>() 
			where TPanel : APanel
		{
			return GetPanelPrefab(typeof(TPanel)) as TPanel;
		}

		protected abstract UICanvas GetCanvasPrefab();
		protected abstract APanel GetPanelPrefab(Type panelType);
	}
}
