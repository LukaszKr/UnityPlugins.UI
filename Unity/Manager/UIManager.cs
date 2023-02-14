using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public class UIManager : AUIManager
	{
		[SerializeField]
		private UICanvas m_Canvas = null;

		[SerializeField]
		private List<APanelRegistry> m_Registries = new List<APanelRegistry>();

		private readonly List<APanelRegistry> m_RuntimeRegistries = new List<APanelRegistry>();

		protected override UICanvas GetCanvasPrefab()
		{
			return m_Canvas;
		}

		protected override APanel GetPanelPrefab(Type panelType)
		{
			APanel panel = SearchForPanel(panelType, m_Registries);
			if(panel != null)
			{
				return panel;
			}

			return SearchForPanel(panelType, m_RuntimeRegistries);
		}

		private APanel SearchForPanel(Type panelType, List<APanelRegistry> registries)
		{
			int count = registries.Count;
			for(int x = 0; x < count; ++x)
			{
				APanelRegistry registry = registries[x];
				APanel panelPrefab = registry.FindPanelPrefab(panelType);
				if(panelPrefab)
				{
					return panelPrefab;
				}
			}
			return null;
		}

		public void AddRuntimeRegistry(APanelRegistry registry)
		{
			m_RuntimeRegistries.Add(registry);
		}

		public bool RemoveRuntimeRegistry(APanelRegistry registry)
		{
			return m_RuntimeRegistries.Remove(registry);
		}
	}
}