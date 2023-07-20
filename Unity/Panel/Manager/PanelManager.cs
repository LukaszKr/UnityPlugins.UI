using System;
using System.Collections.Generic;

namespace ProceduralLevel.UI.Unity
{
	public class PanelManager
	{
		private readonly List<PanelManagerEntry> m_Entries = new List<PanelManagerEntry>();

		public IReadOnlyList<PanelManagerEntry> Entries => m_Entries;

		public void HideAll()
		{
			int count = m_Entries.Count;
			for(int x = count-1; x >= 0; --x)
			{
				m_Entries[x].Panel.Hide();
			}
		}

		internal void Add(APanel panel, UICanvas canvas)
		{
			int index = IndexOf(panel);
			if(index >= 0)
			{
				throw new Exception();
			}
			PanelManagerEntry entry = new PanelManagerEntry(panel, canvas);
			int sortingOrder = GetNextSortOrder();
			canvas.SortingOrder = sortingOrder;
			m_Entries.Add(entry);
		}

		internal void Remove(APanel panel)
		{
			int index = IndexOf(panel);
			m_Entries.RemoveAt(index);
		}

		private int IndexOf(APanel panel)
		{
			int count = m_Entries.Count;
			for(int x = count-1; x >= 0; --x)
			{
				PanelManagerEntry entry = m_Entries[x];
				if(entry.Panel == panel)
				{
					return x;
				}
			}
			return -1;
		}

		private int GetNextSortOrder()
		{
			int count = m_Entries.Count;
			int maxOrder = 0;
			for(int x = 0; x < count; ++x)
			{
				PanelManagerEntry entry = m_Entries[x];
				maxOrder = Math.Max(entry.Canvas.SortingOrder, maxOrder);
			}
			return maxOrder+1;
		}
	}
}
