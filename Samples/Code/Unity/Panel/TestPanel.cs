using ProceduralLevel.Common.Event;
using ProceduralLevel.UnityPlugins.UI.Unity;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Example
{
	public class TestPanel : APanel
	{
		[SerializeField]
		private RectTransform m_Container = null;
		[SerializeField]
		private ImmediateUIConfig m_Config = null;

		private ImmediateUI m_UI;

		protected override void OnInitialize(EventBinder binder)
		{
			m_UI = new ImmediateUI(m_Container, m_Config);

			UITextButton textButton = m_UI.TextButton();
			textButton.Text.SetText("Hello World");
			Bind(binder, textButton);
			UIIconButton iconButton = m_UI.IconButton();
			Bind(binder, iconButton);

			m_UI.Label("Label Example");

		}

		public new void Show()
		{
			base.Show();
		}

		private void Bind(EventBinder binder, AInteractivePanelElement element)
		{
			binder.Bind(element.OnActive, (active) => Debug.Log($"Active: {element.name}, {active}"));
			binder.Bind(element.OnHovered, (hovered) => Debug.Log($"Hovered: {element.name}, {hovered}"));
			binder.Bind(element.OnClicked, () => Debug.Log($"Clicked: {element.name}"));
		}
	}
}
