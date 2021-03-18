namespace ProceduralLevel.UnityPlugins.CustomUI
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
