using ProceduralLevel.Common.Event;
using UnityEngine.EventSystems;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public class PanelElement: AUIElement, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		private EInteractionState m_State = EInteractionState.Idle;

		public EInteractionState State { get { return m_State; } }

		public readonly CustomEvent<EInteractionState> OnStateChanged = new CustomEvent<EInteractionState>();

		public readonly CustomEvent<bool> OnHovered = new CustomEvent<bool>();
		public readonly CustomEvent<bool> OnActive = new CustomEvent<bool>();
		public readonly CustomEvent<bool> OnSelected = new CustomEvent<bool>();

		public readonly CustomEvent OnClick = new CustomEvent();

		protected override void OnPrepare(EventBinder binder)
		{
		}

		protected override void OnCleanup()
		{
		}

		#region State
		public bool SetHovered(bool hovered)
		{
			if(SetState(m_State.SetFlag(EInteractionState.Hovered, hovered)))
			{
				OnHovered.Invoke(hovered);
				return true;
			}
			return false;
		}

		public bool SetActive(bool active)
		{
			if(SetState(m_State.SetFlag(EInteractionState.Active, active)))
			{
				OnActive.Invoke(active);
				if(!active && m_State.IsHovered())
				{
					OnClick.Invoke();
				}
				return true;
			}
			return false;
		}

		public bool SetSelected(bool selected)
		{
			if(SetState(m_State.SetFlag(EInteractionState.Selected, selected)))
			{
				OnSelected.Invoke(selected);
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

		#region Pointer
		public void OnPointerUp(PointerEventData eventData)
		{
			SetActive(false);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			SetActive(true);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			SetHovered(false);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			SetHovered(true);
		}
		#endregion
	}
}
