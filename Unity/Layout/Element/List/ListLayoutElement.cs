using System;
using System.Collections.Generic;

namespace ProceduralLevel.UI.Unity
{
	public class ListLayoutElement : LayoutElement, ILayoutGroupElement
	{
		public ELayoutOrientation Orientation = ELayoutOrientation.Vertical;
		public int GapSize = 5;

		public bool ShouldStretchElement = true;

		private readonly List<ListLayoutEntry> m_Entries = new List<ListLayoutEntry>();

		public IEnumerable<LayoutElement> GetElements()
		{
			foreach(ListLayoutEntry entry in m_Entries)
			{
				yield return entry.Element;
			}
		}

		public void DoLayout()
		{
			int count = m_Entries.Count;
			int offset = 0;
			for(int x = 0; x < count; ++x)
			{
				if(x > 0)
				{
					offset += GapSize;
				}
				ListLayoutEntry entry = m_Entries[x];
				int elementSize = GetElementSize(entry.Element);
				SEtElementPosition(entry.Element, offset);
				if(ShouldStretchElement)
				{
					StretchElement(entry.Element);
				}
				offset += elementSize;
			}
		}

		public ListLayoutEntry Add(LayoutElement element)
		{
			ListLayoutEntry entry = new ListLayoutEntry(element);
			m_Entries.Add(entry);
			return entry;
		}

		private int GetElementSize(LayoutElement element)
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

		private int SetElementSize(LayoutElement element)
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

		private void SEtElementPosition(LayoutElement element, int position)
		{
			switch(Orientation)
			{
				case ELayoutOrientation.Horizontal:
					element.Rect.X = position;
					break;
				case ELayoutOrientation.Vertical:
					element.Rect.Y = position;
					break;
				default:
					throw new NotImplementedException(Orientation.ToString());
			}
		}

		private void StretchElement(LayoutElement element)
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
	}
}
