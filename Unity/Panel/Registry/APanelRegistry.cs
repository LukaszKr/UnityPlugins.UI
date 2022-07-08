using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public abstract class APanelRegistry : ScriptableObject
	{
		public abstract TPanel GetPanelPrefab<TPanel>()
			where TPanel : APanel;
	}
}
