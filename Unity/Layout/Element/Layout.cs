using System;
using System.Collections.Generic;
using ProceduralLevel.Common.Event;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class Layout
	{
		public readonly Layout Parent;
		public LayoutRect Rect = new LayoutRect(0, 0, 10, 10);
		public ELayoutOrientation Orientation = ELayoutOrientation.Vertical;
		public int ElementsSpacing = 5;
		public bool StretchWithChildren = true;
		public float Align = 0f;
		public bool Active = true;

		public ELayoutType ElementType = ELayoutType.Flexible;
		public int ElementSize = 1;
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
			int count = CountActive();
			if(count == 0)
			{
				OnChanged.Invoke();
				return;
			}

			int availableSpace = Rect.GetSize(Orientation);
			int staticSum = SumValues(ELayoutType.Static);
			int gapSpace = (count-1)*ElementsSpacing;
			int flexibleSum = SumValues(ELayoutType.Flexible);
			availableSpace -= staticSum;
			availableSpace -= gapSpace;
			int perFlexibleUnit = 0;
			int usedSpace = 0;
			if(flexibleSum > 0)
			{
				perFlexibleUnit = Mathf.CeilToInt(availableSpace/(float)flexibleSum);
			}
			else
			{
				usedSpace += (int)(Align*availableSpace);
			}

			ELayoutOrientation otherOrientation = Orientation.GetOther();

			for(int x = 0; x < count; ++x)
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
					int expandTo = Rect.GetSize(otherOrientation);
					layout.Rect.SetSize(otherOrientation, expandTo);
				}
				int layoutSize = layout.GetValue(Orientation, perFlexibleUnit);
				layout.Rect.SetSize(Orientation, layoutSize);
				SetPosition(layout, usedSpace);
				layout.DoLayout();
				usedSpace += layoutSize;
			}
			if(StretchWithChildren)
			{
				Rect.SetSize(Orientation, usedSpace);
			}

			OnChanged.Invoke();
		}

		private void SetPosition(Layout layout, int value)
		{
			switch(Orientation)
			{
				case ELayoutOrientation.Horizontal:
					layout.Rect.X = value;
					layout.Rect.Y = 0;
					break;
				case ELayoutOrientation.Vertical:
					layout.Rect.X = 0;
					layout.Rect.Y = value;
					break;
				default:
					throw new NotImplementedException(Orientation.ToString());
			}
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

		private int SumValues(ELayoutType type)
		{
			int sum = 0;
			int count = m_Childrens.Count;
			for(int x = 0; x < count; ++x)
			{
				Layout layout = m_Childrens[x];
				if(layout.Active && layout.ElementType == type)
				{
					sum += layout.GetValue(Orientation);
				}
			}
			return sum;
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

		public int GetValue(ELayoutOrientation orientation, int flexibleMultiplier = 1)
		{
			switch(ElementType)
			{
				case ELayoutType.Flexible:
					return ElementSize * flexibleMultiplier;
				case ELayoutType.Static:
					if(ElementSize == 0)
					{
						return Rect.GetSize(orientation);
					}
					return ElementSize;
				default:
					throw new NotImplementedException(orientation.ToString());
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
