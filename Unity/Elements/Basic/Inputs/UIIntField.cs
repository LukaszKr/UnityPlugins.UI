using UnityPlugins.Common.Logic;
using TMPro;

namespace UnityPlugins.UI.Unity
{
	public class UIIntField : AUIInputField<int>
	{
		protected override void OnInitialize(EventBinder binder)
		{
			base.OnInitialize(binder);

			m_InputField.contentType = TMP_InputField.ContentType.IntegerNumber;
		}

		protected override int StringToValue(string raw)
		{
			int value;
			int.TryParse(raw, out value);
			return value;
		}

		protected override string ValueToString(int value)
		{
			return value.ToString();
		}
	}
}
