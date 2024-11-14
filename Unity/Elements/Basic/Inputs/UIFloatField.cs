using System.Globalization;
using UnityPlugins.Common.Logic;
using TMPro;

namespace UnityPlugins.UI.Unity
{
	public class UIFloatField : AUIInputField<float>
	{
		protected override void OnInitialize(EventBinder binder)
		{
			base.OnInitialize(binder);

			m_InputField.contentType = TMP_InputField.ContentType.DecimalNumber;
		}

		protected override float StringToValue(string raw)
		{
			float value;
			float.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
			return value;
		}

		protected override string ValueToString(float value)
		{
			return value.ToString();
		}
	}
}
