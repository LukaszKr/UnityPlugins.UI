using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity
{
	public class UIToggle : ActiveUIElement
	{
		private bool m_Value;

		public bool Value => m_Value;

		public readonly CustomEvent<bool> OnValueChanged = new CustomEvent<bool>();

		#region Element
		protected override void OnInitialize(EventBinder binder)
		{
			base.OnInitialize(binder);

			binder.Bind(OnClicked, OnClickHandler);
		}
		#endregion

		public void Toggle()
		{
			SetValue(!m_Value);
		}

		public void SetValue(bool value)
		{
			m_Value = value;
			OnValueChanged.Invoke(m_Value);
		}

		#region Callbacks
		private void OnClickHandler()
		{
			SetValue(!m_Value);
		}
		#endregion
	}
}
