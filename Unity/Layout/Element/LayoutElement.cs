using System;
using System.Collections.Generic;
using ProceduralLevel.Common.Unity;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class LayoutElement
	{
		public LayoutRect Rect;
		public ELayoutOrientation Orientation = ELayoutOrientation.Horizontal;
		public int GapSize = 5;
		public bool ShouldExpand = true;

		private readonly List<LayoutEntry> m_Childrens = new List<LayoutEntry>();

		public LayoutElement(int width = 0, int height = 20)
		{
			Rect = new LayoutRect(0, 0, width, height);
		}

		public LayoutElement(ELayoutOrientation orientation)
		{
			Orientation = orientation;
		}

		public LayoutElement(ELayoutOrientation orientation, int size)
		{
			Orientation = orientation;
			Rect.SetSize(orientation.GetOther(), size);
		}

		public IEnumerable<LayoutElement> GetChildrens()
		{
			int count = m_Childrens.Count;
			for(int x = 0; x < count; ++x)
			{
				yield return m_Childrens[x].Element;
			}
		}

		#region Layout
		public void DoLayout()
		{
			int count = m_Childrens.Count;
			if(count == 0)
			{
				return;
			}

			int availableSpace = Rect.GetSize(Orientation);
			int staticSum = SumValues(ELayoutEntryType.Static);
			int gapSpace = (count-1)*GapSize;
			int flexibleSum = SumValues(ELayoutEntryType.Flexible);
			availableSpace -= staticSum;
			availableSpace -= gapSpace;
			int perFlexibleUnit = 0;
			if(flexibleSum > 0)
			{
				perFlexibleUnit = Mathf.CeilToInt(availableSpace/(float)flexibleSum);
			}

			ELayoutOrientation otherOrientation = Orientation.GetOther();

			int usedSpace = 0;
			for(int x = 0; x < count; ++x)
			{
				if(x > 0)
				{
					usedSpace += GapSize;
				}
				LayoutEntry entry = m_Childrens[x];
				LayoutElement element = entry.Element;
				if(entry.Expand)
				{
					int expandTo = Rect.GetSize(otherOrientation);
					element.Rect.SetSize(otherOrientation, expandTo);
				}
				element.DoLayout();
				SetPosition(element, usedSpace);
				int elementSize = entry.GetValue(Orientation, perFlexibleUnit);
				element.Rect.SetSize(Orientation, elementSize);
				usedSpace += elementSize;
			}
			if(ShouldExpand)
			{
				Rect.SetSize(Orientation, usedSpace);
			}
		}

		private void SetPosition(LayoutElement element, int value)
		{
			switch(Orientation)
			{
				case ELayoutOrientation.Horizontal:
					element.Rect.X = value;
					element.Rect.Y = 0;
					break;
				case ELayoutOrientation.Vertical:
					element.Rect.X = 0;
					element.Rect.Y = value;
					break;
				default:
					throw new NotImplementedException(Orientation.ToString());
			}
		}

		private int SumValues(ELayoutEntryType type)
		{
			int sum = 0;
			int count = m_Childrens.Count;
			for(int x = 0; x < count; ++x)
			{
				LayoutEntry entry = m_Childrens[x];
				if(entry.Type == type)
				{
					sum += entry.GetValue(Orientation);
				}
			}
			return sum;
		}
		#endregion

		public LayoutElement AddFlexible(LayoutElement element, int value = 1)
		{
			Add(new LayoutEntry(element, ELayoutEntryType.Flexible, value));
			return element;
		}

		public LayoutElement AddFlexible(int value = 1)
		{
			return AddFlexible(new LayoutElement(), value);
		}

		public LayoutElement AddStatic(LayoutElement element, int value)
		{
			Add(new LayoutEntry(element, ELayoutEntryType.Static, value));
			return element;
		}

		public LayoutElement AddStatic(LayoutElement element)
		{
			return AddStatic(element, 0);
		}

		public LayoutElement AddStatic(int value)
		{
			return AddStatic(new LayoutElement(), value);
		}

		public void Add(LayoutEntry entry)
		{
			m_Childrens.Add(entry);
		}

		public void UseOnGUI()
		{
			GUIExt.PushMatrix(GUI.matrix*Matrix4x4.Translate(new Vector3(Rect.X, Rect.Y)));
		}
	}
}
