using System;
using System.Collections.Generic;
using UnityPlugins.Common.Unity;
using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	public class UIManager : AUnitySingleton<UIManager>
	{
		public readonly PanelsManager PanelsManager = new PanelsManager();

		[SerializeField]
		private UICanvas m_Canvas = null;
		[SerializeField]
		private CanvasGroup m_GlobalCanvasGroup = null;
		[SerializeField]
		private List<APanelProvider> m_PanelProviders = new List<APanelProvider>();

		private bool m_UIShown = true;
		private readonly List<APanelProvider> m_RuntimeProviders = new List<APanelProvider>();

		private readonly List<APanel> m_SpawnedPanels = new List<APanel>();

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
				UICanvas canvas = Instantiate(m_Canvas, Transform, false);
				APanel spawnedPanel = Instantiate(panelPrefab, canvas.Transform);
				spawnedPanel.Setup(canvas, PanelsManager);
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

		public void ToggleUI()
		{
			m_UIShown = !m_UIShown;
			float targetAlpha = (m_UIShown? 1f: 0f);
			m_GlobalCanvasGroup.alpha = targetAlpha;
		}

		#region Panel Prefabs
		protected TPanel GetPanelPrefab<TPanel>()
			where TPanel : APanel
		{
			return GetPanelPrefab(typeof(TPanel)) as TPanel;
		}

		protected APanel GetPanelPrefab(Type panelType)
		{
			APanel panel = GetPanelPrefab(panelType, m_PanelProviders);
			if(panel != null)
			{
				return panel;
			}

			return GetPanelPrefab(panelType, m_RuntimeProviders);
		}

		private APanel GetPanelPrefab(Type panelType, List<APanelProvider> panelProviders)
		{
			int count = panelProviders.Count;
			for(int x = 0; x < count; ++x)
			{
				APanelProvider provider = panelProviders[x];
				APanel panelPrefab = provider.FindPanelPrefab(panelType);
				if(panelPrefab)
				{
					return panelPrefab;
				}
			}
			return null;
		}
		#endregion

		#region Panel Providers
		public void AddProvider(APanelProvider provider)
		{
			m_RuntimeProviders.Add(provider);
		}

		public bool RemoveProvider(APanelProvider provider)
		{
			return m_RuntimeProviders.Remove(provider);
		}
		#endregion
	}
}
