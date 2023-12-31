using System;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public struct LayoutRect : IEquatable<LayoutRect>
	{
		public int X;
		public int Y;
		public int Width;
		public int Height;

		public static bool operator ==(LayoutRect left, LayoutRect right) => left.Equals(right);
		public static bool operator !=(LayoutRect left, LayoutRect right) => !left.Equals(right);

		public static implicit operator Rect(LayoutRect rect) => rect.ToUnity();

		public LayoutRect(Rect rect)
		{
			X = (int)rect.x;
			Y = (int)rect.y;
			Width = (int)rect.width;
			Height = (int)rect.height;
		}

		public LayoutRect(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public void SetSize(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public void SetSize(ELayoutAxis axis, int value)
		{
			switch(axis)
			{
				case ELayoutAxis.Horizontal:
					Width = value;
					break;
				case ELayoutAxis.Vertical:
					Height = value;
					break;
				default:
					throw new NotImplementedException();
			}
		}

		public int GetSize(ELayoutAxis orientation)
		{
			switch(orientation)
			{
				case ELayoutAxis.Horizontal:
					return Width;
				case ELayoutAxis.Vertical:
					return Height;
				default:
					throw new NotImplementedException(orientation.ToString());
			}
		}

		public Rect ToUnity()
		{
			return new Rect(X, Y, Width, Height);
		}

		public override bool Equals(object obj)
		{
			if(obj is LayoutRect other)
			{
				return Equals(other);
			}
			return false;
		}

		public bool Equals(LayoutRect other)
		{
			return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y, Width, Height);
		}

		public override string ToString()
		{
			return $"({nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Width)}: {Width}, {nameof(Height)}: {Height})";
		}
	}
}
