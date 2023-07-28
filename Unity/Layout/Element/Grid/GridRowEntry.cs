namespace ProceduralLevel.UI.Unity
{
	public class GridRowEntry
	{
		public readonly LayoutElement Element;
		public int Width;

		public GridRowEntry(LayoutElement element, int width)
		{
			Element = element;
			Width = width;
		}
	}
}
