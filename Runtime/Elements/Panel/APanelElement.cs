using ProceduralLevel.Common.Event;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public abstract class APanelElement: AUIElement
	{
		private AUIPanel m_Panel;
		private EInteractableState m_State = EInteractableState.Idle;

		public readonly PointerHandler Pointer = new PointerHandler();

		public EInteractableState State { get { return m_State; } }

		public readonly CustomEvent<APanelElement,EInteractableState> OnStateChanged = new CustomEvent<APanelElement, EInteractableState>();

		public readonly CustomEvent<APanelElement, bool> OnHovered = new CustomEvent<APanelElement, bool>();
		public readonly CustomEvent<APanelElement, bool> OnFocused = new CustomEvent<APanelElement, bool>();
		public readonly CustomEvent<APanelElement, bool> OnActive = new CustomEvent<APanelElement, bool>();
		public readonly CustomEvent<APanelElement, bool> OnSelected = new CustomEvent<APanelElement, bool>();

		protected override void OnPrepare(EventBinder binder)
		{
			AUIPanel panel = Transform.GetComponentInParent<AUIPanel>();
			m_Panel = panel;
			panel.AddElement(this);
		}

		protected override void OnCleanup()
		{
			m_Panel.RemoveElement(this);
			m_Panel = null;
		}

		public void Update()
		{
			if(Pointer.UpdateStatus() && !Pointer.IsActive())
			{
				SetActive(false);
			}
		}

		#region State
		public bool SetHovered(bool hovered)
		{
			if(SetState(m_State.SetFlag(EInteractableState.Hovered, hovered)))
			{
				OnHovered.Invoke(this, hovered);
				return true;
			}
			return false;
		}

		public bool SetFocused(bool focused)
		{
			if(SetState(m_State.SetFlag(EInteractableState.Focused, focused)))
			{
				OnFocused.Invoke(this, focused);
				return true;
			}
			return false;
		}

		public bool SetActive(bool active)
		{
			if(SetState(m_State.SetFlag(EInteractableState.Active, active)))
			{
				OnActive.Invoke(this, active);
				return true;
			}
			return false;
		}

		public bool SetSelected(bool selected)
		{
			if(SetState(m_State.SetFlag(EInteractableState.Selected, selected)))
			{
				OnSelected.Invoke(this, selected);
				return true;
			}
			return false;
		}

		private bool SetState(EInteractableState newState)
		{
			if(m_State != newState)
			{
				m_State = newState;
				OnStateChanged.Invoke(this, m_State);
				return true;
			}
			return false;
		}
		#endregion
	}
}
