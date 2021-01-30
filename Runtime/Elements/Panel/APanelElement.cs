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

		public readonly CustomEvent<EInteractableState> OnStateChanged = new CustomEvent<EInteractableState>();

		public readonly CustomEvent<bool> OnHovered = new CustomEvent<bool>();
		public readonly CustomEvent<bool> OnFocused = new CustomEvent<bool>();
		public readonly CustomEvent<bool> OnActive = new CustomEvent<bool>();
		public readonly CustomEvent<bool> OnSelected = new CustomEvent<bool>();

		protected override void Awake()
		{
			base.Awake();
			TryPrepare();
		}

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
			Pointer.UpdateStatus();
		}

		#region State
		public bool SetHovered(bool hovered)
		{
			if(SetState(m_State.SetFlag(EInteractableState.Hovered, hovered)))
			{
				OnHovered.Invoke( hovered);
				return true;
			}
			return false;
		}

		public bool SetFocused(bool focused)
		{
			if(SetState(m_State.SetFlag(EInteractableState.Focused, focused)))
			{
				OnFocused.Invoke(focused);
				return true;
			}
			return false;
		}

		public bool SetActive(bool active)
		{
			if(SetState(m_State.SetFlag(EInteractableState.Active, active)))
			{
				OnActive.Invoke(active);
				return true;
			}
			return false;
		}

		public bool SetSelected(bool selected)
		{
			if(SetState(m_State.SetFlag(EInteractableState.Selected, selected)))
			{
				OnSelected.Invoke(selected);
				return true;
			}
			return false;
		}

		private bool SetState(EInteractableState newState)
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
	}
}
