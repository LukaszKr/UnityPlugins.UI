using System;
using System.Collections.Generic;
using ProceduralLevel.Common.Event;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class Layout
	{
		public readonly Layout Parent;

		public LayoutMargin Margin;
		public LayoutVector Size;
		public LayoutRect Rect;

		//Properties
		public bool FitToChildren = false;
		public float Align = 0f;
		public bool Active = true;

		//Child Layout
		public ELayoutAxis Axis = ELayoutAxis.Vertical;
		public int ElementsSpacing = 5;

		//Element Dimensions
		public ELayoutMode LayoutMode = ELayoutMode.Flexible;
		public int LayoutModeSize = 1;
		public bool ExpandToParent = true;

		private readonly List<Layout> m_Childrens = new List<Layout>();

		public readonly CustomEvent OnChanged = new CustomEvent();

		public Layout(Layout parent)
		{
			Parent = parent;
		}

		public void Destroy()
		{
			if(Parent != null)
			{
				Parent.Remove(this);
			}
			OnChanged.RemoveAllListeners();
		}

		#region Layout
		public void DoLayout()
		{
			DoLayout(new LayoutVector());
		}

		private void DoLayout(LayoutVector offset)
		{
			int activeElementCount = CountActive();
			int px = offset.X+Margin.Left;
			int py = offset.Y+Margin.Top;
			int width = Size.X-Margin.Horizontal;
			int height = Size.Y-Margin.Vertical;
			Rect = new LayoutRect(px, py, width, height);

			int availableSpace = Rect.GetSize(Axis);
			int staticSum = SumElementLayoutSizes(ELayoutMode.Static);
			int gapSpace = (activeElementCount-1)*ElementsSpacing;
			int flexibleSum = SumElementLayoutSizes(ELayoutMode.Flexible);
			availableSpace -= staticSum;
			availableSpace -= gapSpace;
			float flexibleUnit = 0;
			float usedSpace = 0;
			if(flexibleSum > 0)
			{
				flexibleUnit = availableSpace/(float)flexibleSum;
			}
			else
			{
				usedSpace += (int)(Align*availableSpace);
			}

			ELayoutAxis otherAxis = Axis.GetOther();

			for(int x = 0; x < activeElementCount; ++x)
			{
				Layout layout = m_Childrens[x];
				if(!layout.Active)
				{
					continue;
				}

				if(x > 0)
				{
					usedSpace += ElementsSpacing;
				}

				if(layout.ExpandToParent)
				{
					int expandTo = Rect.GetSize(otherAxis);
					layout.Size.SetValue(otherAxis, expandTo);
				}

				float layoutSize = layout.CalculateElementSize(Axis, flexibleUnit);

				layout.Size.SetValue(Axis, Mathf.CeilToInt(layoutSize));
				LayoutVector childOffset = new LayoutVector(Axis, Mathf.FloorToInt(usedSpace), 0);
				layout.DoLayout(childOffset);
				usedSpace += layoutSize;
			}
			if(FitToChildren)
			{
				Rect.SetSize(Axis, Mathf.CeilToInt(usedSpace));
			}

			OnChanged.Invoke();
		}

		private int CountActive()
		{
			int count = m_Childrens.Count;
			int activeCount = 0;
			for(int x = 0; x < count; ++x)
			{
				if(m_Childrens[x].Active)
				{
					activeCount++;
				}
			}
			return activeCount;
		}

		private int SumElementLayoutSizes(ELayoutMode mode)
		{
			float sum = 0;
			int count = m_Childrens.Count;
			for(int x = 0; x < count; ++x)
			{
				Layout layout = m_Childrens[x];
				if(layout.Active && layout.LayoutMode == mode)
				{
					sum += layout.CalculateElementSize(Axis);
				}
			}
			return (int)sum;
		}
		#endregion

		private int IndexOf(Layout layout)
		{
			int count = m_Childrens.Count;
			for(int x = 0; x < count; ++x)
			{
				if(m_Childrens[x] == layout)
				{
					return x;
				}
			}
			return -1;
		}

		private bool Remove(Layout layout)
		{
			int index = IndexOf(layout);
			if(index == -1)
			{
				return false;
			}
			m_Childrens.RemoveAt(index);
			return true;
		}

		private float CalculateElementSize(ELayoutAxis axis, float flexibleMultiplier = 1)
		{
			switch(LayoutMode)
			{
				case ELayoutMode.Flexible:
					return LayoutModeSize * flexibleMultiplier;
				case ELayoutMode.Static:
					return LayoutModeSize;
				default:
					throw new NotImplementedException(axis.ToString());
			}
		}

		public Layout CreateChild()
		{
			Layout layout = new Layout(this);
			m_Childrens.Add(layout);
			return layout;
		}
	}
}
