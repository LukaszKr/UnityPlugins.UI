namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public interface IPanelRegistry
	{
		TPanel GetPanel<TPanel>()
			where TPanel : AUIPanel;
	}
}
