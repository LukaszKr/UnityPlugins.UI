using System;
using System.Collections.Generic;
using UnityEngine;
using UnityPlugins.Common.Unity;

namespace UnityPlugins.UI.Unity
{
	public class UIManagerComponent : ExtendedMonoBehaviour
	{
		public readonly PanelsManager PanelsManager = new PanelsManager();

		[SerializeField]
		private Transform m_PanelsContainer = null;
		[SerializeField]
		private UICanvasComponent m_Canvas = null;
		[SerializeField]
		private List<APanelProviderSO> m_PanelProviders = new List<APanelProviderSO>();

		private readonly List<APanelProviderSO> m_RuntimeProviders = new List<APanelProviderSO>();

		private readonly List<APanelComponent> m_SpawnedPanels = new List<APanelComponent>();

		public TPanel GetPanel<TPanel>()
			where TPanel : APanelComponent
		{
			return (TPanel)GetPanel(typeof(TPanel));
		}

		public APanelComponent GetPanel(Type panelType)
		{
			APanelComponent existingPanel = FindPanel(panelType);
			if(existingPanel != null)
			{
				return existingPanel;
			}

			APanelComponent panelPrefab = GetPanelPrefab(panelType);
			if(panelPrefab != null)
			{
				UICanvasComponent canvas = Instantiate(m_Canvas, m_PanelsContainer, false);
				APanelComponent spawnedPanel = Instantiate(panelPrefab, canvas.Transform);
				spawnedPanel.Setup(canvas, PanelsManager);
				m_SpawnedPanels.Add(spawnedPanel);
				return spawnedPanel;
			}
			throw new NullReferenceException();
		}

		public TPanel FindPanel<TPanel>()
			where TPanel : APanelComponent
		{
			return FindPanel(typeof(TPanel)) as TPanel;
		}

		public APanelComponent FindPanel(Type panelType)
		{
			int count = m_SpawnedPanels.Count;
			for(int x = 0; x < count; ++x)
			{
				APanelComponent panel = m_SpawnedPanels[x];
				if(panel.GetType() == panelType)
				{
					return panel;
				}
			}

			return null;
		}

		#region Panel Prefabs
		protected TPanel GetPanelPrefab<TPanel>()
			where TPanel : APanelComponent
		{
			return GetPanelPrefab(typeof(TPanel)) as TPanel;
		}

		protected APanelComponent GetPanelPrefab(Type panelType)
		{
			APanelComponent panel = GetPanelPrefab(panelType, m_PanelProviders);
			if(panel != null)
			{
				return panel;
			}

			return GetPanelPrefab(panelType, m_RuntimeProviders);
		}

		private APanelComponent GetPanelPrefab(Type panelType, List<APanelProviderSO> panelProviders)
		{
			int count = panelProviders.Count;
			for(int x = 0; x < count; ++x)
			{
				APanelProviderSO provider = panelProviders[x];
				APanelComponent panelPrefab = provider.FindPanelPrefab(panelType);
				if(panelPrefab)
				{
					return panelPrefab;
				}
			}
			return null;
		}
		#endregion

		#region Panel Providers
		public void AddProvider(APanelProviderSO provider)
		{
			m_RuntimeProviders.Add(provider);
		}

		public bool RemoveProvider(APanelProviderSO provider)
		{
			return m_RuntimeProviders.Remove(provider);
		}
		#endregion
	}
}
