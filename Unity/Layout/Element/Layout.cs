using System;
using System.Collections.Generic;
using ProceduralLevel.Common.Unity;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class Layout
	{
		public LayoutRect Rect;
		public ELayoutOrientation Orientation = ELayoutOrientation.Horizontal;
		public int GapSize = 5;
		public bool ShouldExpand = true;

		private readonly List<LayoutEntry> m_Childrens = new List<LayoutEntry>();

		public Layout(ELayoutOrientation orientation = ELayoutOrientation.Vertical, int width = 0, int height = 20)
		{
			Orientation = orientation;
			Rect = new LayoutRect(0, 0, width, height);
		}

		public Layout(ELayoutOrientation orientation, int size)
		{
			Orientation = orientation;
			Rect.SetSize(orientation.GetOther(), size);
		}

		public IEnumerable<Layout> GetChildrens()
		{
			int count = m_Childrens.Count;
			for(int x = 0; x < count; ++x)
			{
				yield return m_Childrens[x].Layout;
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
				Layout layout = entry.Layout;
				if(entry.Expand)
				{
					int expandTo = Rect.GetSize(otherOrientation);
					layout.Rect.SetSize(otherOrientation, expandTo);
				}
				int layoutSize = entry.GetValue(Orientation, perFlexibleUnit);
				layout.Rect.SetSize(Orientation, layoutSize);
				layout.DoLayout();
				SetPosition(layout, usedSpace);
				usedSpace += layoutSize;
			}
			if(ShouldExpand)
			{
				Rect.SetSize(Orientation, usedSpace);
			}
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

		public Layout AddFlexible(Layout layout, int value)
		{
			Add(new LayoutEntry(layout, ELayoutEntryType.Flexible, value));
			return layout;
		}

		public Layout AddFlexible(int value = 1, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
		{
			return AddFlexible(new Layout(orientation), value);
		}

		public Layout AddStatic(Layout layout, int value)
		{
			Add(new LayoutEntry(layout, ELayoutEntryType.Static, value));
			return layout;
		}

		public Layout AddStatic(Layout layout)
		{
			return AddStatic(layout, 0);
		}

		public Layout AddStatic(int value, ELayoutOrientation orientation = ELayoutOrientation.Vertical)
		{
			return AddStatic(new Layout(orientation), value);
		}

		private void Add(LayoutEntry entry)
		{
			m_Childrens.Add(entry);
		}

		public void UseOnGUI()
		{
			GUIExt.PushMatrix(GUI.matrix*Matrix4x4.Translate(new Vector3(Rect.X, Rect.Y)));
		}
	}
}
