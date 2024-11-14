using System;

namespace UnityPlugins.UI.Unity
{
	[Flags]
	public enum EInteractionState : byte
	{
		Idle = 0,

		Hovered = 1 << 1, //pointer is over
		Active = 1 << 2,
		Selected = 1 << 3,
		Enabled = 1 << 4
	}

	public static class EInteractionStateExt
	{
		public static EInteractionState SetFlag(this EInteractionState state, EInteractionState flag, bool enabled)
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

		public static bool Contains(this EInteractionState state, EInteractionState other)
		{
			return other != 0 && (state & other) == other;
		}

		public static bool IsHovered(this EInteractionState state)
		{
			return state.Contains(EInteractionState.Hovered);
		}

		public static bool IsActive(this EInteractionState state)
		{
			return state.Contains(EInteractionState.Active);
		}

		public static bool IsSelected(this EInteractionState state)
		{
			return state.Contains(EInteractionState.Selected);
		}

		public static bool IsEnabled(this EInteractionState state)
		{
			return state.Contains(EInteractionState.Enabled);
		}
	}
}
