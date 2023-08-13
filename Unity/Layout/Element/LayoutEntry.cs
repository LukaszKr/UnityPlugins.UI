using System;

namespace ProceduralLevel.UI.Unity
{
	public class LayoutEntry
	{
		public readonly Layout Layout;
		public readonly ELayoutEntryType Type;
		public int Value;
		public bool Expand = true;

		public LayoutEntry(Layout layout, ELayoutEntryType type, int value)
		{
			Layout = layout;
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
						return Layout.Rect.GetSize(orientation);
					}
					return Value;
				default:
					throw new NotImplementedException(orientation.ToString());
			}
		}

		public override string ToString()
		{
			return $"({Type}, {Value}, {Layout.Rect})";
		}
	}
}
