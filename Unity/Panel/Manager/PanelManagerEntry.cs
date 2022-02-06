namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	internal class PanelManagerEntry
	{
		public readonly APanel Panel;
		public readonly UICanvas Canvas;

		public PanelManagerEntry(APanel panel, UICanvas canvas)
		{
			Panel = panel;
			Canvas = canvas;
		}
	}
}
