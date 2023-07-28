namespace ProceduralLevel.UI.Unity
{
	public class LineLayoutEntry
	{
		public readonly LayoutElement Element;
		public readonly ELayoutEntryType Type;
		public int Value;

		public LineLayoutEntry(LayoutElement element, ELayoutEntryType type, int value)
		{
			Element = element;
			Type = type;
			Value = value;
		}

		public override string ToString()
		{
			return $"({Type}, {Value}, {Element.Rect})";
		}
	}
}
