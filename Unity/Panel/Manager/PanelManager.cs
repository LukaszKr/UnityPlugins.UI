using System;
using System.Collections.Generic;
using ProceduralLevel.UnityPlugins.Common.Unity.Extended;
using ProceduralLevel.UnityPlugins.Input.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public class PanelManager : ExtendedMonoBehaviour
	{
		private int m_UpdateTick;

		private readonly List<PanelManagerEntry> m_Entries = new List<PanelManagerEntry>();
		private readonly List<RaycastResult> m_RaycastResults = new List<RaycastResult>(64);

		private IInteractiveElement m_HoveredElement = null;
		private IInteractiveElement m_ActiveElement = null;

		private AInputDetector m_Interaction;
		private bool m_InteractionActive;

		public void Initialize()
		{
			m_Interaction = new DurationDetector()
				.Add(EMouseInputID.Left)
				.Add(ETouchInputID.Touch01);
		}

		private void Update()
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

		#region Input Detection

		private void UpdatePointer(Vector2 position)
		{
			IInteractiveElement hoveredElement = GetHoveredElement(position);

			if(m_Interaction.Triggered)
			{
				if(!m_InteractionActive && hoveredElement != null && m_ActiveElement == null)
				{
					m_ActiveElement = hoveredElement;
					hoveredElement.InteractionHandler.TrySetActive(true);
				}
				m_InteractionActive = true;
			}
			else
			{
				m_InteractionActive = false;
				if(m_ActiveElement != null)
				{
					m_ActiveElement.InteractionHandler.TrySetActive(false);
					m_ActiveElement = null;
				}
			}


			if(m_HoveredElement != hoveredElement)
			{
				if(m_HoveredElement != null)
				{
					m_HoveredElement.InteractionHandler.TrySetHovered(false);
				}
				m_HoveredElement = hoveredElement;
				if(m_HoveredElement != null)
				{
					hoveredElement.InteractionHandler.TrySetHovered(true);
				}
			}

			if(hoveredElement == null && m_HoveredElement != null)
			{
				m_HoveredElement.InteractionHandler.TrySetHovered(false);
				m_HoveredElement = null;
			}
		}
		#endregion

		public IInteractiveElement GetHoveredElement(Vector2 position)
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
				IInteractiveElement hoveredElement = target.GetComponent<IInteractiveComponent>();
				if(hoveredElement != null)
				{
					return hoveredElement;
				}
				hoveredElement = target.GetComponentInParent<IInteractiveComponent>();
				if(hoveredElement != null)
				{
					return hoveredElement;
				}
			}

			return null;
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
