using ProceduralLevel.Common.Event;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public abstract class APanelElement: AUIElement
	{
		private AUIPanel m_Panel;
		private EInteractableState m_State = EInteractableState.Idle;

		public readonly PointerHandler Pointer = new PointerHandler();

		public readonly CustomEvent<EInteractableState> OnStateChanged = new CustomEvent<EInteractableState>();

		public EInteractableState State { get { return m_State; } }

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
			return SetState(m_State.SetFlag(EInteractableState.Hovered, hovered));
		}

		public bool SetFocused(bool focused)
		{
			return SetState(m_State.SetFlag(EInteractableState.Focused, focused));
		}

		public bool SetActive(bool active)
		{
			return SetState(m_State.SetFlag(EInteractableState.Active, active));
		}

		public bool SetSelected(bool selected)
		{
			return SetState(m_State.SetFlag(EInteractableState.Selected, selected));
		}

		private bool SetState(EInteractableState state)
		{
			if(m_State != state)
			{
				name = state.ToString();
				m_State = state;
				OnStateChanged.Invoke(m_State);
				return true;
			}
			return false;
		}
		#endregion
	}
}
