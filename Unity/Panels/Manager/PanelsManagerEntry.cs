namespace UnityPlugins.UI.Unity
{
	public class PanelsManagerEntry
	{
		public readonly APanel Panel;
		public readonly UICanvas Canvas;

		public PanelsManagerEntry(APanel panel, UICanvas canvas)
		{
			Panel = panel;
			Canvas = canvas;
		}
	}
}
