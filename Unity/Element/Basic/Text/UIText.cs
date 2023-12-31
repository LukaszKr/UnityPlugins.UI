using ProceduralLevel.Common.Event;
using ProceduralLevel.Localization.Unity;
using TMPro;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class UIText : AUIElement
	{
		private bool m_IsLocalized;
		private LocalizationKey m_Key;
		private readonly EventBinder m_Binder = new EventBinder();

		[SerializeField]
		private TextMeshProUGUI m_Text = null;

		protected override void OnEnable()
		{
			base.OnEnable();
			m_Binder.Enable();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_Binder.Disable();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			m_Binder.UnbindAll();
		}

		#region Element
		protected override void OnInitialize(EventBinder binder)
		{
		}
		#endregion

		public void SetText(string text)
		{
			m_Text.text = text;
			SetLocalized(false);
		}

		#region Localization
		public void SetLocalization(LocalizationKey key)
		{
			m_Key = key;
			SetLocalized(true);
		}

		private void RefreshLocalization()
		{
			if(m_IsLocalized)
			{
				m_Text.text = m_Key.GetTranslation();
			}
		}

		private void SetLocalized(bool localized)
		{
			if(m_IsLocalized != localized)
			{
				m_IsLocalized = localized;
				if(localized)
				{
					m_Binder.Bind(LocalizationManager.Instance.OnLanguageChanged, OnLanguageChangedHandler);
					RefreshLocalization();
				}
				else
				{
					m_Binder.UnbindAll();
					m_Key = default;
				}
			}
		}
		#endregion

		#region Callbacks
		private void OnLanguageChangedHandler(LanguageConfig config)
		{
			RefreshLocalization();
		}
		#endregion
	}
}
