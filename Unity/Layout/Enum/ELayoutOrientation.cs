namespace ProceduralLevel.UI.Unity
{
	public enum ELayoutOrientation
	{
		Horizontal = 0,
		Vertical = 1,
	}

	public static class ELayoutOrientationExt
	{
		public static ELayoutOrientation GetOther(this ELayoutOrientation orientation)
		{
			if(orientation == ELayoutOrientation.Vertical)
			{
				return ELayoutOrientation.Horizontal;
			}
			return ELayoutOrientation.Vertical;
		}
	}
}
