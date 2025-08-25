using System;
using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	public abstract class APanelProviderSO : ScriptableObject
	{
		public virtual TPanel FindPanelPrefab<TPanel>()
			where TPanel : APanelComponent
		{
			return FindPanelPrefab(typeof(TPanel)) as TPanel;
		}

		public abstract APanelComponent FindPanelPrefab(Type panelType);
	}
}
