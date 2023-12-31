using System;

namespace ProceduralLevel.UI.Unity
{
	public struct LayoutMargin : IEquatable<LayoutMargin>
	{
		public int Top;
		public int Right;
		public int Bottom;
		public int Left;

		public int Horizontal => Left+Right;
		public int Vertical => Top+Bottom;

		public static bool operator ==(LayoutMargin left, LayoutMargin right) => left.Equals(right);
		public static bool operator !=(LayoutMargin left, LayoutMargin right) => !left.Equals(right);

		public LayoutMargin(int top, int right, int bottom, int left)
		{
			Top = top;
			Right = right;
			Bottom = bottom;
			Left = left;
		}

		public LayoutMargin(int horizontal, int vertical)
		{
			Top = vertical;
			Right = horizontal;
			Bottom = vertical;
			Left = horizontal;
		}

		public LayoutMargin(int value)
		{
			Top = value;
			Right = value;
			Bottom = value;
			Left = value;
		}

		public override bool Equals(object obj)
		{
			if(obj is LayoutMargin other)
			{
				return Equals(other);
			}
			return false;
		}

		public bool Equals(LayoutMargin other)
		{
			return Top == other.Top && Right == other.Right && Bottom == other.Bottom && Left == other.Left;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Top, Right, Bottom, Left);
		}

		public override string ToString()
		{
			return $"({nameof(Top)}: {Top}, {nameof(Right)}: {Right}, {nameof(Bottom)}: {Bottom}, {nameof(Left)}: {Left})";
		}
	}
}
