namespace ProceduralLevel.UI.Unity
{
	public class LineLayoutEntry
	{
		public readonly ALayoutElement Element;
		public readonly ELayoutEntryType Type;
		public int Value;

		public LineLayoutEntry(ALayoutElement element, ELayoutEntryType type, int value)
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
