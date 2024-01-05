namespace ProceduralLevel.UI.Unity
{
	public class PanelManagerEntry
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
