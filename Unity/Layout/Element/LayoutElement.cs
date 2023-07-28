namespace ProceduralLevel.UI.Unity
{
	public class LayoutElement
	{
		public LayoutRect Rect;

		public LayoutElement(int width = 0, int height = 20)
		{
			Rect = new LayoutRect(0, 0, width, height);
		}
	}
}
