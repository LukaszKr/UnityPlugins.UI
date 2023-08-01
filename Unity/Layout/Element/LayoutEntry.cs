using System;

namespace ProceduralLevel.UI.Unity
{
	public class LayoutEntry
	{
		public readonly LayoutElement Element;
		public readonly ELayoutEntryType Type;
		public int Value;
		public bool Expand = true;

		public LayoutEntry(LayoutElement element, ELayoutEntryType type, int value)
		{
			Element = element;
			Type = type;
			Value = value;
		}

		public int GetValue(ELayoutOrientation orientation, int flexibleMultiplier = 1)
		{
			switch(Type)
			{
				case ELayoutEntryType.Flexible:
					return Value * flexibleMultiplier;
				case ELayoutEntryType.Static:
					if(Value == 0)
					{
						return Element.Rect.GetSize(orientation);
					}
					return Value;
				default:
					throw new NotImplementedException(orientation.ToString());
			}
		}

		public override string ToString()
		{
			return $"({Type}, {Value}, {Element.Rect})";
		}
	}
}
