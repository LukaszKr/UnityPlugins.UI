using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public class UIManager : AUIManager
	{
		[SerializeField]
		private UICanvas m_Canvas = null;

		[SerializeField]
		private APanelRegistry[] m_Registries = null;

		private readonly List<APanelRegistry> m_RuntimeRegistries = new List<APanelRegistry>();

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

			int count = m_RuntimeRegistries.Count;
			for(int x = 0; x < count; ++x)
			{
				APanelRegistry registry = m_RuntimeRegistries[x];
				TPanel panelPrefab = registry.GetPanelPrefab<TPanel>();
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