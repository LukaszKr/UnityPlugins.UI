using System;

namespace ProceduralLevel.UI.Unity
{
	public struct LayoutVector : IEquatable<LayoutVector>
	{
		public int X;
		public int Y;

		public static bool operator ==(LayoutVector left, LayoutVector right) => left.Equals(right);
		public static bool operator !=(LayoutVector left, LayoutVector right) => !left.Equals(right);

		public LayoutVector(int x, int y)
		{
			X = x;
			Y = y;
		}

		public LayoutVector(ELayoutAxis mainAxis, int a, int b)
		{
			switch(mainAxis)
			{
				case ELayoutAxis.Horizontal:
					X = a;
					Y = b;
					break;
				case ELayoutAxis.Vertical:
					X = b;
					Y = a;
					break;
				default:
					throw new NotImplementedException(mainAxis.ToString());
			}
		}

		public void SetValue(ELayoutAxis axis, int value)
		{
			switch(axis)
			{
				case ELayoutAxis.Horizontal:
					X = value;
					break;
				case ELayoutAxis.Vertical:
					Y = value;
					break;
				default:
					throw new NotImplementedException(axis.ToString());
			}
		}

		public int GetValue(ELayoutAxis orientation)
		{
			switch(orientation)
			{
				case ELayoutAxis.Horizontal:
					return X;
				case ELayoutAxis.Vertical:
					return Y;
				default:
					throw new NotImplementedException(orientation.ToString());
			}
		}

		public override bool Equals(object obj)
		{
			if(obj is LayoutVector other)
			{
				return Equals(other);
			}
			return false;
		}

		public bool Equals(LayoutVector other)
		{
			return X == other.X && Y == other.Y;
		}

		public override int GetHashCode()
		{
			return X + (Y << 16);
		}

		public override string ToString()
		{
			return $"({nameof(X)}: {X}, {nameof(Y)}: {Y})";
		}
	}
}
