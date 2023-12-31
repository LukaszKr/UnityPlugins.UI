namespace ProceduralLevel.UI.Unity
{
	internal class UIIntField : AUIInputField<int>
	{
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
