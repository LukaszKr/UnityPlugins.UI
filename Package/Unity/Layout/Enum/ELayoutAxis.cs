namespace ProceduralLevel.UI.Unity
{
	public enum ELayoutAxis
	{
		Horizontal = 0,
		Vertical = 1,
	}

	public static class ELayoutAxisExt
	{
		public static ELayoutAxis GetOther(this ELayoutAxis axis)
		{
			if(axis == ELayoutAxis.Vertical)
			{
				return ELayoutAxis.Horizontal;
			}
			return ELayoutAxis.Vertical;
		}
	}
}
