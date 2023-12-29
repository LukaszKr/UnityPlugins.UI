using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class UIBuilder
	{
		private readonly Stack<LayoutComponent> m_Stack = new Stack<LayoutComponent>();
		private LayoutComponent m_Current;

		public UIBuilder(Transform parent)
		{
			m_Current = LayoutComponent.Create("Root", parent);
		}

		public LayoutComponent BeginGroup(string name, ELayoutOrientation orientation)
		{
			m_Stack.Push(m_Current);
			return m_Current;
		}

		public void EndGroup()
		{
			m_Current = m_Stack.Pop();
		}

		public LayoutComponent BeginVertical(string name)
		{
			return BeginGroup(name, ELayoutOrientation.Vertical);
		}

		public void EndVertical()
		{
			EndGroup();
		}

		public LayoutComponent BeginHorizontal(string name)
		{
			return BeginGroup(name, ELayoutOrientation.Horizontal);
		}

		public void EndHorizontal()
		{
			EndGroup();
		}
	}
}
