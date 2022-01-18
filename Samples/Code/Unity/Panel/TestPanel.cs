using ProceduralLevel.Common.Event;
using ProceduralLevel.UnityPlugins.UI.Unity;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Example
{
	public class TestPanel : APanel
	{
		protected override void OnPrepare(EventBinder binder)
		{
			IInteractiveComponent[] elements = GetComponentsInChildren<IInteractiveComponent>();
			int length = elements.Length;
			for(int x = 0; x < length; ++x)
			{
				Bind(binder, elements[x]);
			}
		}

		public new void Show()
		{
			base.Show();
		}

		private void Bind(EventBinder binder, IInteractiveComponent element)
		{
			InteractionHandler handler = element.InteractionHandler;
			binder.Bind(handler.OnActive, (active) => Debug.Log($"Active: {element.name}, {active}"));
			binder.Bind(handler.OnHovered, (hovered) => Debug.Log($"Hovered: {element.name}, {hovered}"));
			binder.Bind(handler.OnClick, () => Debug.Log($"Clicked: {element.name}"));
		}
	}
}
