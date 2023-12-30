using ProceduralLevel.Common.Event;
using UnityEngine;
using TMPro;
using ProceduralLevel.Localization.Unity;

namespace ProceduralLevel.UI.Unity
{
	public class UIText : AUIElement
	{
		private bool m_IsLocalized;
		private LocalizationKey m_Key;

		[SerializeField]
		private TextMeshProUGUI m_Text = null;

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
					LocalizationManager.Instance.OnLanguageChanged.AddListener(OnLanguageChangedHandler);
					RefreshLocalization();
				}
				else
				{
					LocalizationManager.Instance.OnLanguageChanged.RemoveListener(OnLanguageChangedHandler);
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
