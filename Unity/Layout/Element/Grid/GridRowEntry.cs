namespace ProceduralLevel.UI.Unity
{
	public class GridRowEntry
	{
		public readonly ALayoutElement Element;
		public int Width;

		public GridRowEntry(ALayoutElement element, int width)
		{
			Element = element;
			Width = width;
		}
	}
}
