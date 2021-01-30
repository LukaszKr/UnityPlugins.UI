using System;
using System.Collections.Generic;
using ProceduralLevel.UnityPlugins.Common.Extended;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public abstract class APanelManager: ExtendedMonoBehaviour
	{
		private const int BUFFER_SIZE = 32;

		private readonly SortedList<int, PanelManagerEntry> m_Entries = new SortedList<int, PanelManagerEntry>();

		private readonly APanelElement[] m_ElementBuffer = new APanelElement[BUFFER_SIZE];

		private APanelElement m_Hovered = null;
		private APanelElement m_Focused = null;
		private APanelElement m_Active = null;

		public APanelElement Hovered { get { return m_Hovered; } }
		public APanelElement Focused { get { return m_Focused; } }
		public APanelElement Active { get { return m_Active; } }

		private void Update()
		{
			UpdatePointer();
			if(m_Active)
			{
				if(!m_Active.Pointer.IsActive())
				{
					m_Active = null;
				}
			}
		}

		#region Pointer
		protected abstract void UpdatePointer();

		public void UsePointer(EPointerType pointerType)
		{
			if(m_Focused != m_Hovered)
			{
				if(m_Focused != null)
				{
					m_Focused.SetFocused(false);
				}
				m_Focused = m_Hovered;
				if(m_Focused != null)
				{
					m_Focused.SetFocused(true);
				}
			}

			if(m_Hovered != null)
			{
				m_Active = m_Hovered;
			}
			if(m_Active)
			{
				m_Active.SetActive(true);
				m_Active.Pointer.Use(pointerType);
			}
		}

		public void SetPointerPosition(Vector2 vector)
		{
			IList<PanelManagerEntry> entries = m_Entries.Values;
			int count = entries.Count;
			for(int x = count-1; x >= 0; --x)
			{
				PanelManagerEntry entry = entries[x];
				int offset = entry.Panel.GetElementsAt(vector, m_ElementBuffer);
				if(offset > 0)
				{
					UpdateHovered(m_ElementBuffer[0]);

					for(int y = 0; y < offset; ++y)
					{
						m_ElementBuffer[y] = null;
					}
					return;
				}
			}
			UpdateHovered(null);
		}

		private void UpdateHovered(APanelElement element)
		{
			if(m_Hovered != element)
			{
				if(m_Hovered)
				{
					m_Hovered.SetHovered(false);
				}
				if(element)
				{
					element.SetHovered(true);
				}
				m_Hovered = element;
			}
		}
		#endregion

		internal void Add(AUIPanel panel, UICanvas canvas)
		{
			int index = IndexOf(panel);
			if(index >= 0)
			{
				throw new Exception();
			}
			PanelManagerEntry entry = new PanelManagerEntry(panel, canvas);
			int sortingOrder = GetNextSortOrder();
			canvas.SortingOrder = sortingOrder;
			m_Entries.Add(sortingOrder, entry);
		}

		internal void Remove(AUIPanel panel)
		{
			int index = IndexOf(panel);
			m_Entries.RemoveAt(index);
		}

		private int IndexOf(AUIPanel panel)
		{
			int count = m_Entries.Count;
			for(int x = 0; x < count; ++x)
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
