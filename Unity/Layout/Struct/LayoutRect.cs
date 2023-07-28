using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public struct LayoutRect
	{
		public int X;
		public int Y;
		public int Width;
		public int Height;

		public static implicit operator Rect(LayoutRect rect) => rect.ToUnity();

		public LayoutRect(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public Rect ToUnity()
		{
			return new Rect(X, Y, Width, Height);
		}

		public override string ToString()
		{
			return $"({nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Width)}: {Width}, {nameof(Height)}: {Height})";
		}
	}
}
