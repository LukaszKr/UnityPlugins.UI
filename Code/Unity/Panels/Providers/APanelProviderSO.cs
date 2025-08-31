using System;
using UnityEngine;
using UnityPlugins.Common.Unity;

namespace UnityPlugins.UI.Unity
{
	public abstract class APanelProviderSO : AExtendedSO
	{
		public virtual TPanel FindPanelPrefab<TPanel>()
			where TPanel : APanelComponent
		{
			return FindPanelPrefab(typeof(TPanel)) as TPanel;
		}

		public abstract APanelComponent FindPanelPrefab(Type panelType);
	}
}
