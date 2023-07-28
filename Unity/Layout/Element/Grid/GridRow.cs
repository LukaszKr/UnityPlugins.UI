using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class GridRow
	{
		public LayoutRect Rect;
		public bool LineBreak;

		public readonly GridLayoutElement Grid;
		public readonly List<GridRowEntry> Entries = new List<GridRowEntry>();
		public int UsedWidth;

		public GridRow(GridLayoutElement grid, bool lineBreak)
		{
			Grid = grid;
			LineBreak = lineBreak;
		}

		public void Update(int columnWidth, int columnGap)
		{
			int count = Entries.Count;
			int totalWidth = 0;
			int maxHeight = 0;
			bool isLast;

			for(int x = 0; x < count; ++x)
			{
				isLast = (x == count-1);
				GridRowEntry entry = Entries[x];
				ALayoutElement element = entry.Element;
				if(element is ILayoutGroupElement layoutElement)
				{
					layoutElement.DoLayout();
				}

				float rawWidth = entry.Width*columnWidth;
				int width = (int)rawWidth;
				width += (entry.Width-1)*columnGap;
				element.Rect.X = totalWidth;
				element.Rect.Y = Rect.Y;
				element.Rect.Width = width;
				maxHeight = Mathf.Max(element.Rect.Height, maxHeight);
				totalWidth += width;
				if(!isLast)
				{
					totalWidth += columnGap;
				}

				if(totalWidth >= Rect.Width)
				{
					element.Rect.Width -= (totalWidth-Rect.Width);
				}
			}

			Rect.Height = maxHeight;
		}

		public void CollapseInto(GridRow target)
		{
			while(Entries.Count > 0)
			{
				GridRowEntry entry = Entries[0];
				if(target.CanAllocate(entry.Width, true))
				{
					target.Add(entry);
					RemoveEntry(0, entry);
					continue;
				}

				break;
			}
		}

		public bool CanAllocate(int width, bool allowOverflowIfEmpty = false)
		{
			return UsedWidth+width <= Grid.ColumnCount || UsedWidth == 0 && allowOverflowIfEmpty;
		}

		public GridRowEntry Add(ALayoutElement element, int width)
		{
			GridRowEntry entry = new GridRowEntry(element, width);
			Entries.Add(entry);
			UsedWidth += width;
			return entry;
		}

		public void AddFirst(GridRowEntry entry)
		{
			Entries.Insert(0, entry);
			UsedWidth += entry.Width;
		}

		public void Add(GridRowEntry entry)
		{
			Entries.Add(entry);
			UsedWidth += entry.Width;
		}

		private void RemoveEntry(int index, GridRowEntry entry)
		{
			Entries.RemoveAt(index);
			UsedWidth -= entry.Width;
		}
	}
}
