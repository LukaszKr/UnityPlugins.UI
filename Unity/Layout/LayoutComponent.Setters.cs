namespace ProceduralLevel.UI.Unity
{
	public partial class LayoutComponent
	{
		#region Simple Setters
		public LayoutComponent SetAlign(float align)
		{
			m_Layout.Align = align;
			return this;
		}

		public LayoutComponent SetActive(bool active)
		{
			m_Layout.Active = active;
			return this;
		}

		public LayoutComponent SetElementsSpacing(int elementsSpacing)
		{
			m_Layout.ElementsSpacing = elementsSpacing;
			return this;
		}

		public LayoutComponent SetFitToChildren(bool fitToChildren)
		{
			m_Layout.FitToChildren = fitToChildren;
			return this;
		}

		public LayoutComponent SetExpandToParent(bool expand)
		{
			m_Layout.ExpandToParent = expand;
			return this;
		}
		#endregion

		#region Margin
		public LayoutComponent SetMargin(LayoutMargin margin)
		{
			m_Layout.Margin = margin;
			return this;
		}

		public LayoutComponent SetMargin(int top, int right, int bottom, int left)
		{
			return SetMargin(new LayoutMargin(top, right, bottom, left));
		}

		public LayoutComponent SetMargin(int horizontal, int vertical)
		{
			return SetMargin(new LayoutMargin(horizontal, vertical));
		}

		public LayoutComponent SetMargin(int margin)
		{
			return SetMargin(new LayoutMargin(margin));
		}
		#endregion

		#region Orientation
		public LayoutComponent SetAxis(ELayoutAxis axis)
		{
			m_Layout.Axis = axis;
			return this;
		}

		public LayoutComponent SetVertical()
		{
			return SetAxis(ELayoutAxis.Vertical);
		}

		public LayoutComponent SetHorizontal()
		{
			return SetAxis(ELayoutAxis.Horizontal);
		}
		#endregion

		#region Type
		public LayoutComponent SetType(ELayoutMode type, int size = 1)
		{
			m_Layout.LayoutMode = type;
			m_Layout.LayoutModeSize = size;
			return this;
		}

		public LayoutComponent SetFlexible(int flexibleUnits = 1)
		{
			return SetType(ELayoutMode.Flexible, flexibleUnits);
		}

		public LayoutComponent SetStatic(int staticSize)
		{
			return SetType(ELayoutMode.Static, staticSize);
		}
		#endregion

		#region Size
		public LayoutComponent SetSize(int width, int height)
		{
			m_Layout.Size = new LayoutVector(width, height);
			return this;
		}

		public LayoutComponent SetDimension(ELayoutAxis axis, int value)
		{
			m_Layout.Size.SetValue(axis, value);
			return this;
		}

		public LayoutComponent SetHeight(int height)
		{
			return SetDimension(ELayoutAxis.Vertical, height);
		}

		public LayoutComponent SetWidth(int width)
		{
			return SetDimension(ELayoutAxis.Horizontal, width);
		}
		#endregion
	}
}
