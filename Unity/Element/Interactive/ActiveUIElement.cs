using ProceduralLevel.Common.Event;
using UnityEngine.EventSystems;

namespace ProceduralLevel.UI.Unity
{
	public class ActiveUIElement : AUIElement, 
		IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
	{
		private EInteractionState m_State = EInteractionState.Enabled;

		public readonly CustomEvent<EInteractionState> OnStateChanged = new CustomEvent<EInteractionState>();

		public readonly CustomEvent<bool> OnHovered = new CustomEvent<bool>();
		public readonly CustomEvent<bool> OnActive = new CustomEvent<bool>();
		public readonly CustomEvent<bool> OnSelected = new CustomEvent<bool>();
		public readonly CustomEvent<bool> OnEnabled = new CustomEvent<bool>();

		public readonly CustomEvent OnClicked = new CustomEvent();

		public EInteractionState State => m_State;

		public bool IsHovered => m_State.Contains(EInteractionState.Hovered);
		public bool IsActive => m_State.Contains(EInteractionState.Active);
		public bool IsSelected => m_State.Contains(EInteractionState.Selected);
		public bool IsEnabled => m_State.Contains(EInteractionState.Enabled);

		protected override void OnInitialize(EventBinder binder)
		{
		}

		#region State
		public bool TrySetHovered(bool hovered)
		{
			if((IsEnabled || !hovered) && SetState(m_State.SetFlag(EInteractionState.Hovered, hovered)))
			{
				OnHovered.Invoke(hovered);
				return true;
			}
			return false;
		}

		public bool TrySetActive(bool active)
		{
			if((IsEnabled || !active) && SetState(m_State.SetFlag(EInteractionState.Active, active)))
			{
				OnActive.Invoke(active);
				if(!active && m_State.IsHovered())
				{
					OnClicked.Invoke();
				}
				return true;
			}
			return false;
		}

		public bool TrySetSelected(bool selected)
		{
			if(SetState(m_State.SetFlag(EInteractionState.Selected, selected)))
			{
				OnSelected.Invoke(selected);
				return true;
			}
			return false;
		}

		public bool TrySetEnabled(bool enabled)
		{
			if(SetState(m_State.SetFlag(EInteractionState.Enabled, enabled)))
			{
				OnEnabled.Invoke(enabled);
				return true;
			}
			return false;
		}

		private bool SetState(EInteractionState newState)
		{
			if(m_State != newState)
			{
				m_State = newState;
				OnStateChanged.Invoke(m_State);
				return true;
			}
			return false;
		}
		#endregion

		#region Pointer Methods
		public void OnPointerDown(PointerEventData eventData)
		{
			TrySetActive(true);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			TrySetActive(false);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			TrySetHovered(true);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			TrySetHovered(false);
		}
		#endregion
	}
}
