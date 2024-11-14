using UnityPlugins.Common.Logic;
using TMPro;

namespace UnityPlugins.UI.Unity
{
	public class UITextField : AUIInputField<string>
	{
		protected override void OnInitialize(EventBinder binder)
		{
			base.OnInitialize(binder);

			m_InputField.contentType = TMP_InputField.ContentType.Standard;
		}

		protected override string StringToValue(string raw)
		{
			return raw;
		}

		protected override string ValueToString(string value)
		{
			return value;
		}
	}
}
