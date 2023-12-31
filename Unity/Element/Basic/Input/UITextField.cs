namespace ProceduralLevel.UI.Unity
{
	public class UITextField : AUIInputField<string>
	{
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
