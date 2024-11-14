using System;
using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	public abstract class APanelProvider : ScriptableObject
	{
		public virtual TPanel FindPanelPrefab<TPanel>()
			where TPanel : APanel
		{
			return FindPanelPrefab(typeof(TPanel)) as TPanel;
		}

		public abstract APanel FindPanelPrefab(Type panelType);
	}
}
