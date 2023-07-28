namespace ProceduralLevel.UI.Unity
{
	public abstract class ALayoutElement
	{
		public LayoutRect Rect;

		public ALayoutElement(int width = 0, int height = 20)
		{
			Rect = new LayoutRect(0, 0, width, height);
		}

		public ALayoutElement SetRect(LayoutRect rect)
		{
			Rect = rect;
			return this;
		}
	}
}
