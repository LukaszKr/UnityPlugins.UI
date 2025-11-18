namespace UnityPlugins.UI.Unity
{
	public class PanelsManagerEntry
	{
		public readonly APanelComponent Panel;
		public readonly UICanvasComponent Canvas;

		public PanelsManagerEntry(APanelComponent panel, UICanvasComponent canvas)
		{
			Panel = panel;
			Canvas = canvas;
		}
	}
}
