using ProceduralLevel.UnityPlugins.Input;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public class DefaultPanelManager: APanelManager
	{
		[SerializeField]
		private InputManager m_InputManager = null;

		protected override void UpdatePointer()
		{
			MouseDevice mouse = m_InputManager.Mouse;
			if(mouse.IsActive)
			{
				SetPointerPosition(mouse.Position);
				if(mouse.Get(EMouseInputID.Left).IsActive)
				{
					UsePointer(EPointerType.Primary);
				}
				if(mouse.Get(EMouseInputID.Right).IsActive)
				{
					UsePointer(EPointerType.Secondary);
				}
				if(mouse.Get(EMouseInputID.Middle).IsActive)
				{
					UsePointer(EPointerType.Tertiary);
				}
			}

			TouchDevice touch = m_InputManager.Touch;
			if(touch.IsActive)
			{
				int count = touch.Count;
				SetPointerPosition(touch.Touches[0].Position);
				UsePointer((EPointerType)Mathf.Min(count, EPointerTypeExt.Meta.MaxValue-1));
			}
		}
	}
}
