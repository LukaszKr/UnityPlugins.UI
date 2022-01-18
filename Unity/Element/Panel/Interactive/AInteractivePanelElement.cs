namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public abstract class AInteractivePanelElement : APanelElement, IInteractiveComponent
	{
		public InteractionHandler InteractionHandler { get; } = new InteractionHandler();
	}
}
