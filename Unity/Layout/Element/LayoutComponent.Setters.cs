using UnityEngine;

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

		public LayoutComponent SetRect(Rect rect)
		{
			m_Layout.Rect = new LayoutRect(rect);
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

		#region Size
		public LayoutComponent SetDimension(ELayoutAxis axis, int value)
		{
			m_Layout.Rect.SetSize(axis, value);
			return this;
		}

		public LayoutComponent SetHeight(int value)
		{
			return SetDimension(ELayoutAxis.Vertical, value);
		}

		public LayoutComponent SetWidth(int value)
		{
			return SetDimension(ELayoutAxis.Horizontal, value);
		}
		#endregion
	}
}
