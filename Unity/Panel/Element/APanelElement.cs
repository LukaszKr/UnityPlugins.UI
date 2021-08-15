namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public abstract class APanelElement : AUIElement
	{
		protected override void Awake()
		{
			base.Awake();
			TryPrepare();
		}
	}
}
