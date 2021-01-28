using System;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	[Flags]
	public enum EInteractableState: byte
	{
		Idle = 0,

		Hovered = 1 << 1, //pointer is over
		Focused = 1 << 2,
		Active = 1 << 3,
		Selected = 1 << 4,
	}

	public static class EInteractableStateExt
	{
		public static EInteractableState SetFlag(this EInteractableState state, EInteractableState flag, bool enabled)
		{
			if(enabled)
			{
				return state | flag;
			}
			else
			{
				return state & ~flag;
			}
		}

		public static bool Contains(this EInteractableState state, EInteractableState other)
		{
			return other != 0 && (state & other) == other;
		}

		public static bool IsHovered(this EInteractableState state)
		{
			return state.Contains(EInteractableState.Hovered);
		}

		public static bool IsFocused(this EInteractableState state)
		{
			return state.Contains(EInteractableState.Focused);
		}

		public static bool IsActive(this EInteractableState state)
		{
			return state.Contains(EInteractableState.Active);
		}

		public static bool IsSelected(this EInteractableState state)
		{
			return state.Contains(EInteractableState.Selected);
		}
	}
}
