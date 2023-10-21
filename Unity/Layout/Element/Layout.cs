using System;
using System.Collections.Generic;
using ProceduralLevel.Common.Event;
using ProceduralLevel.Common.Unity;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class Layout
	{
		public LayoutRect Rect;
		public ELayoutOrientation Orientation;
		public int GapSize = 5;
		public bool StretchToChildrenSize = true;
		public float Align = 0f;
		public bool Active = true;

		public ELayoutEntryType ElementType;
		public int ElementSize;
		public bool ExpandToParent = true;

		private readonly List<Layout> m_Childrens = new List<Layout>();

		public readonly CustomEvent OnChanged = new CustomEvent();

		public Layout(ELayoutOrientation orientation = ELayoutOrientation.Horizontal, ELayoutEntryType elementType = ELayoutEntryType.Flexible, int elementSize = 1)
		{
			Orientation = orientation;
			Rect = new LayoutRect(0, 0, 10, 10);
			ElementType = elementType;
			ElementSize = elementSize;
		}

		public IEnumerable<Layout> GetChildrens()
		{
			int count = m_Childrens.Count;
			for(int x = 0; x < count; ++x)
			{
				yield return m_Childrens[x];
			}
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
			int staticSum = SumValues(ELayoutEntryType.Static);
			int gapSpace = (count-1)*GapSize;
			int flexibleSum = SumValues(ELayoutEntryType.Flexible);
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
					usedSpace += GapSize;
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
			if(StretchToChildrenSize)
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

		private int SumValues(ELayoutEntryType type)
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

		public int IndexOf(Layout layout)
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

		public bool Remove(Layout layout)
		{
			int index = IndexOf(layout);
			if(index == -1)
			{
				return false;
			}
			m_Childrens.RemoveAt(index);
			return true;
		}

		public void Clear()
		{
			m_Childrens.Clear();
		}

		public int GetValue(ELayoutOrientation orientation, int flexibleMultiplier = 1)
		{
			switch(ElementType)
			{
				case ELayoutEntryType.Flexible:
					return ElementSize * flexibleMultiplier;
				case ELayoutEntryType.Static:
					if(ElementSize == 0)
					{
						return Rect.GetSize(orientation);
					}
					return ElementSize;
				default:
					throw new NotImplementedException(orientation.ToString());
			}
		}

		public Layout AddFlexible(int value = 1, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
		{
			Layout layout = new Layout(orientation, ELayoutEntryType.Flexible, value);
			m_Childrens.Add(layout);
			return layout;
		}

		public Layout AddStatic(int value, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
		{
			Layout layout = new Layout(orientation, ELayoutEntryType.Static, value);
			m_Childrens.Add(layout);
			return layout;
		}

		public void UseOnGUI()
		{
			GUIExt.PushMatrix(GUI.matrix*Matrix4x4.Translate(new Vector3(Rect.X, Rect.Y)));
		}
	}
}
