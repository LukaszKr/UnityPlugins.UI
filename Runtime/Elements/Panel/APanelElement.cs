namespace ProceduralLevel.UnityPlugins.UI
{
	public abstract class APanelElement: AUIElement
	{
		protected override void Awake()
		{
			base.Awake();
			TryPrepare();
		}
	}
}
