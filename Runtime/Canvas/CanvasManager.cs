using ProceduralLevel.UnityPlugins.Common.Extended;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public class CanvasManager: ExtendedMonoBehaviour
	{
		private const int BUFFER_SIZE = 32;

		private readonly SortedList<int, CanvasManagerEntry> m_Entries = new SortedList<int, CanvasManagerEntry>();

		private readonly APanelElement[] m_ElementBuffer = new APanelElement[BUFFER_SIZE];

		private APanelElement m_Hovered = null;
		private APanelElement m_Active = null;

		public void Update()
		{
			Vector2 vector = Input.mousePosition;
			SetPointerPosition(Input.mousePosition);

			if(Input.GetMouseButton(0))
			{
				UsePointer(EPointerType.Primary);
			}
			if(m_Active)
			{
				if(!m_Active.Pointer.IsActive())
				{
					m_Active = null;
				}
			}
		}

		#region Pointer
		public void UsePointer(EPointerType pointerType)
		{
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
			IList<CanvasManagerEntry> entries = m_Entries.Values;
			int count = entries.Count;
			for(int x = count-1; x >= 0; --x)
			{
				CanvasManagerEntry entry = entries[x];
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

		public void Add(AUIPanel panel, UICanvas canvas)
		{
			int index = IndexOf(panel);
			if(index >= 0)
			{
				throw new Exception();
			}
			CanvasManagerEntry entry = new CanvasManagerEntry(panel, canvas);
			int sortingOrder = GetNextSortOrder();
			canvas.SortingOrder = sortingOrder;
			m_Entries.Add(sortingOrder, entry);
		}

		public void Remove(AUIPanel panel)
		{
			int index = IndexOf(panel);
			m_Entries.RemoveAt(index);
		}

		private CanvasManagerEntry GetEntry(AUIPanel panel)
		{
			int index = IndexOf(panel);
			return m_Entries[index];
		}

		private int IndexOf(AUIPanel panel)
		{
			int count = m_Entries.Count;
			for(int x = 0; x < count; ++x)
			{
				CanvasManagerEntry entry = m_Entries[x];
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
				CanvasManagerEntry entry = m_Entries[x];
				maxOrder = Math.Max(entry.Canvas.SortingOrder, maxOrder);
			}
			return maxOrder+1;
		}
	}
}
