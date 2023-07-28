using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class GridLayoutElement : LayoutElement, ILayoutGroupElement
	{
		private int m_ColumnCount;
		private int m_ColumnGap = 5;
		private int m_RowGap = 5;

		public bool ShouldTrimHeight = true;

		private GridRow m_CurrentRow;
		private List<GridRow> m_Rows = new List<GridRow>();

		public int ColumnCount => m_ColumnCount;

		public GridLayoutElement(int columnCount)
		{
			m_ColumnCount = columnCount;
		}

		public IEnumerable<LayoutElement> GetElements()
		{
			int rowCount = m_Rows.Count;
			for(int rowIndex = 0; rowIndex < rowCount; ++rowIndex)
			{
				GridRow row = m_Rows[rowIndex];
				int elementCount = row.Entries.Count;
				for(int elementIndex = 0; elementIndex < elementCount; ++elementIndex)
				{
					LayoutElement element = row.Entries[elementIndex].Element;
					yield return element;
				}
			}
		}

		public void DoLayout()
		{
			int availableWidth = Rect.Width;
			int totalGapSpace = (m_ColumnCount-1)*m_ColumnGap;
			int availableColumnWidth = availableWidth-totalGapSpace;
			int columnWidth = Mathf.CeilToInt(availableColumnWidth/(float)m_ColumnCount);

			int usedHeight = 0;
			int rowCount = m_Rows.Count;
			for(int x = 0; x < rowCount; ++x)
			{
				if(x > 0)
				{
					usedHeight += m_RowGap;
				}
				GridRow row = m_Rows[x];
				row.Rect.Y = usedHeight;
				row.Rect.Width = availableWidth;
				row.Update(columnWidth, m_ColumnGap);
				usedHeight += row.Rect.Height;
			}

			if(ShouldTrimHeight)
			{
				Rect.Height = usedHeight;
			}
		}

		public void SetColumnCount(int columnCount)
		{
			if(m_ColumnCount == columnCount)
			{
				return;
			}

			m_ColumnCount = columnCount;

			if(m_Rows.Count == 0)
			{
				return;
			}

			List<GridRow> oldRows = m_Rows;
			m_Rows = new List<GridRow>();
			m_CurrentRow = null;

			int count = oldRows.Count;
			for(int x = 0; x < count; ++x)
			{
				GridRow oldRow = oldRows[x];
				if(oldRow.LineBreak || m_CurrentRow == null)
				{
					StartNewRow(oldRow.LineBreak);
				}
				while(oldRow.Entries.Count > 0)
				{
					oldRow.CollapseInto(m_CurrentRow);
					if(oldRow.Entries.Count == 0)
					{
						break;
					}
					else
					{
						StartNewRow(false);
					}
				}
			}
		}

		public GridRowEntry Add(LayoutElement element, int width)
		{
			if(m_CurrentRow == null || m_CurrentRow.UsedWidth+width > m_ColumnCount)
			{
				StartNewRow(false);
			}
			return m_CurrentRow.Add(element, width);
		}

		public GridRow StartNewRow()
		{
			return StartNewRow(true);
		}

		private GridRow StartNewRow(bool lineBreak = true)
		{
			m_CurrentRow = new GridRow(this, lineBreak);
			m_Rows.Add(m_CurrentRow);
			return m_CurrentRow;
		}
	}
}
