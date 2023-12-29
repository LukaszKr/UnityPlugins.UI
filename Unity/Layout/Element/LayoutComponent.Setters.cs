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

		public LayoutComponent SetRect(LayoutRect rect)
		{
			m_Layout.Rect = rect;
			return this;
		}

		public LayoutComponent SetElementsSpacing(int elementsSpacing)
		{
			m_Layout.ElementsSpacing = elementsSpacing;
			return this;
		}

		public LayoutComponent SetStretchWithChildren(bool stretch)
		{
			m_Layout.StretchWithChildren = stretch;
			return this;
		}

		public LayoutComponent SetExpandToParent(bool expand)
		{
			m_Layout.ExpandToParent = expand;
			return this;
		}
		#endregion

		#region Orientation
		public LayoutComponent SetOrientation(ELayoutOrientation orientation)
		{
			m_Layout.Orientation = orientation;
			return this;
		}

		public LayoutComponent SetVertical()
		{
			return SetOrientation(ELayoutOrientation.Vertical);
		}

		public LayoutComponent SetHorizontal()
		{
			return SetOrientation(ELayoutOrientation.Horizontal);
		}
		#endregion

		#region Type
		public LayoutComponent SetType(ELayoutType type, int size = 1)
		{
			m_Layout.ElementType = type;
			m_Layout.ElementSize = size;
			return this;
		}

		public LayoutComponent SetFlexible(int size = 1)
		{
			return SetType(ELayoutType.Flexible, size);
		}

		public LayoutComponent SetStatic(int size)
		{
			return SetType(ELayoutType.Static, size);
		}
		#endregion
	}
}
