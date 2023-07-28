using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class LineLayoutElement : LayoutElement, ILayoutGroupElement
	{
		public ELayoutOrientation Orientation = ELayoutOrientation.Horizontal;
		public int GapSize = 5;
		public bool ShouldExpandElement = true;

		private readonly List<LineLayoutEntry> m_Entries = new List<LineLayoutEntry>();

		public IEnumerable<LayoutElement> GetElements()
		{
			int count = m_Entries.Count;
			for(int x = 0; x < count; ++x)
			{
				yield return m_Entries[x].Element;
			}
		}

		public void DoLayout()
		{
			int count = m_Entries.Count;
			if(count == 0)
			{
				return;
			}

			int availableSpace = GetDimension(this);
			int staticSum = SumValues(ELayoutEntryType.Static);
			int gapSpace = (count-1)*GapSize;
			int flexibleSum = SumValues(ELayoutEntryType.Flexible);
			availableSpace -= staticSum;
			availableSpace -= gapSpace;
			int perFlexibleUnit = Mathf.CeilToInt(availableSpace/(float)flexibleSum);

			int usedSpace = 0;
			for(int x = 0; x < count; ++x)
			{
				if(x > 0)
				{
					usedSpace += GapSize;
				}
				LineLayoutEntry entry = m_Entries[x];
				LayoutElement element = entry.Element;
				if(element is ILayoutGroupElement groupElement)
				{
					groupElement.DoLayout();
				}
				element.Rect.X = usedSpace;
				int elementSize;
				switch(entry.Type)
				{
					case ELayoutEntryType.Flexible:
						elementSize = entry.Value*perFlexibleUnit;
						break;
					case ELayoutEntryType.Static:
						elementSize = entry.Value;
						break;
					default:
						throw new NotImplementedException(entry.Type.ToString());
				}
				SetDimension(element, elementSize);
				usedSpace += elementSize;
				if(ShouldExpandElement)
				{
					ExpandElement(element);
				}
			}
		}

		protected void ExpandElement(LayoutElement element)
		{
			switch(Orientation)
			{
				case ELayoutOrientation.Horizontal:
					element.Rect.Height = Rect.Height;
					break;
				case ELayoutOrientation.Vertical:
					element.Rect.Width = Rect.Width;
					break;
				default:
					throw new NotImplementedException(Orientation.ToString());
			}
		}

		protected int GetDimension(LayoutElement element)
		{
			switch(Orientation)
			{
				case ELayoutOrientation.Horizontal:
					return element.Rect.Width;
				case ELayoutOrientation.Vertical:
					return element.Rect.Height;
				default:
					throw new NotImplementedException(Orientation.ToString());
			}
		}

		protected void SetDimension(LayoutElement element, int value)
		{
			switch(Orientation)
			{
				case ELayoutOrientation.Horizontal:
					element.Rect.Width = value;
					break;
				case ELayoutOrientation.Vertical:
					element.Rect.Height = value;
					break;
				default:
					throw new NotImplementedException(Orientation.ToString());
			}
		}

		private int SumValues(ELayoutEntryType type)
		{
			int sum = 0;
			int count = m_Entries.Count;
			for(int x = 0; x < count; ++x)
			{
				LineLayoutEntry entry = m_Entries[x];
				if(entry.Type == type)
				{
					sum += entry.Value;
				}
			}
			return sum;
		}

		public LayoutElement AddFlexible(int value)
		{
			LayoutElement element = new LayoutElement();
			Add(new LineLayoutEntry(element, ELayoutEntryType.Flexible, value));
			return element;
		}

		public LayoutElement AddStatic(int value)
		{
			LayoutElement element = new LayoutElement();
			Add(new LineLayoutEntry(element, ELayoutEntryType.Static, value));
			return element;
		}

		public void Add(LineLayoutEntry entry)
		{
			m_Entries.Add(entry);
		}
	}
}
