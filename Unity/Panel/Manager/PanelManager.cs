using System;
using System.Collections.Generic;
using ProceduralLevel.UnityPlugins.Input.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public class PanelManager
	{
		private int m_UpdateTick;

		private readonly List<PanelManagerEntry> m_Entries = new List<PanelManagerEntry>();
		private readonly List<RaycastResult> m_RaycastResults = new List<RaycastResult>(64);

		private AInteractivePanelElement m_HoveredElement = null;
		private AInteractivePanelElement m_ActiveElement = null;

		private readonly AInputDetector m_Interaction;
		private bool m_InteractionActive;

		public PanelManager()
		{
			m_Interaction = new DurationDetector()
				.Add(EMouseInputID.Left)
				.Add(ETouchInputID.Touch01);
		}

		internal void Update()
		{
			m_Interaction.Update(m_UpdateTick++, Time.deltaTime);

			MouseDevice mouse = MouseDevice.Instance;
			if(mouse.IsActive)
			{
				UpdatePointer(mouse.Position);
			}
			else if(TouchDevice.Instance.IsActive)
			{
				TouchDevice touchDevice = TouchDevice.Instance;
				TouchData touch = touchDevice.Touches[0];
				UpdatePointer(touch.Position);
			}
		}

		#region Input
		private void UpdatePointer(Vector2 position)
		{
			AInteractivePanelElement hoveredElement = GetHoveredElement(position);

			if(m_Interaction.Active)
			{
				if(!m_InteractionActive && hoveredElement != null && m_ActiveElement == null)
				{
					m_ActiveElement = hoveredElement;
					hoveredElement.TrySetActive(true);
				}
				m_InteractionActive = true;
			}
			else
			{
				m_InteractionActive = false;
				if(m_ActiveElement != null)
				{
					m_ActiveElement.TrySetActive(false);
					m_ActiveElement = null;
				}
			}

			if(m_HoveredElement != hoveredElement)
			{
				if(m_HoveredElement != null)
				{
					m_HoveredElement.TrySetHovered(false);
				}
				m_HoveredElement = hoveredElement;
				if(m_HoveredElement != null)
				{
					hoveredElement.TrySetHovered(true);
				}
			}
		}

		public AInteractivePanelElement GetHoveredElement(Vector2 position)
		{
			m_RaycastResults.Clear();
			PointerEventData eventData = new PointerEventData(null);
			eventData.position = position;
			int lastIndex = m_Entries.Count-1;

			for(int x = lastIndex; x >= 0; --x)
			{
				PanelManagerEntry entry = m_Entries[x];
				UICanvas canvas = entry.Canvas;
				GraphicRaycaster raycaster = canvas.Raycaster;
				raycaster.Raycast(eventData, m_RaycastResults);
				if(m_RaycastResults.Count > 0)
				{
					break;
				}
			}

			int count = m_RaycastResults.Count;

			for(int x = 0; x < count; ++x)
			{
				RaycastResult result = m_RaycastResults[x];
				GameObject target = result.gameObject;
				AInteractivePanelElement hoveredElement = target.GetComponent<AInteractivePanelElement>();
				if(hoveredElement != null)
				{
					return hoveredElement;
				}
				hoveredElement = target.GetComponentInParent<AInteractivePanelElement>();
				if(hoveredElement != null)
				{
					return hoveredElement;
				}
			}

			return null;
		}
		#endregion

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
