using System;
using ProceduralLevel.Common.Collision2D;
using UnityEditor.Experimental.GraphView;
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

		public LayoutRect(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public void SetPosition(ELayoutOrientation orientation, int value)
		{
			switch(orientation)
			{
				case ELayoutOrientation.Horizontal:
					X = value;
					break;
				case ELayoutOrientation.Vertical:
					Y = value;
					break;
				default:
					throw new NotImplementedException();
			}
		}

		public void SetSize(ELayoutOrientation orientation, int value)
		{
			switch(orientation)
			{
				case ELayoutOrientation.Horizontal:
					Width = value;
					break;
				case ELayoutOrientation.Vertical:
					Height = value;
					break;
				default:
					throw new NotImplementedException();
			}
		}

		public int GetSize(ELayoutOrientation orientation)
		{
			switch(orientation)
			{
				case ELayoutOrientation.Horizontal:
					return Width;
				case ELayoutOrientation.Vertical:
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
