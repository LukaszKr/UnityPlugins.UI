namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public abstract class APanelElement : AElement
	{
		protected override void Awake()
		{
			base.Awake();
			TryPrepare();
		}
	}
}
