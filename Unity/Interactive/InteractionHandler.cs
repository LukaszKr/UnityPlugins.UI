using ProceduralLevel.Common.Event;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public class InteractionHandler
	{
		private EInteractionState m_State = EInteractionState.Enabled;

		public readonly CustomEvent<EInteractionState> OnStateChanged = new CustomEvent<EInteractionState>();

		public readonly CustomEvent<bool> OnHovered = new CustomEvent<bool>();
		public readonly CustomEvent<bool> OnActive = new CustomEvent<bool>();
		public readonly CustomEvent<bool> OnSelected = new CustomEvent<bool>();
		public readonly CustomEvent<bool> OnEnabled = new CustomEvent<bool>();

		public readonly CustomEvent OnClick = new CustomEvent();

		public EInteractionState State => m_State;

		public bool IsHovered => m_State.Contains(EInteractionState.Hovered);
		public bool IsActive => m_State.Contains(EInteractionState.Active);
		public bool IsSelected => m_State.Contains(EInteractionState.Selected);
		public bool IsEnabled => m_State.Contains(EInteractionState.Enabled);


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
					OnClick.Invoke();
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
	}
}
