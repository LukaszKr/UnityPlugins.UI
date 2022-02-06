namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public class PanelManagerEntry
	{
		public readonly APanel Panel;
		public readonly UICanvas Canvas;

		internal PanelManagerEntry(APanel panel, UICanvas canvas)
		{
			Panel = panel;
			Canvas = canvas;
		}
	}
}
