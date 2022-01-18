using ProceduralLevel.Common.Event;
using ProceduralLevel.UnityPlugins.UI.Unity;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Example
{
	public class TestPanel : APanel
	{
		protected override void OnPrepare(EventBinder binder)
		{
			AInteractivePanelElement[] elements = GetComponentsInChildren<AInteractivePanelElement>();
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

		private void Bind(EventBinder binder, AInteractivePanelElement element)
		{
			binder.Bind(element.OnActive, (active) => Debug.Log($"Active: {element.name}, {active}"));
			binder.Bind(element.OnHovered, (hovered) => Debug.Log($"Hovered: {element.name}, {hovered}"));
			binder.Bind(element.OnClick, () => Debug.Log($"Clicked: {element.name}"));
		}
	}
}
