using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class UIBuilder
	{
		public readonly LayoutComponent Root;

		private readonly Stack<LayoutComponent> m_Stack = new Stack<LayoutComponent>();
		private LayoutComponent m_Current;

		public UIBuilder(Transform parent, string name = "Root")
		{
			Root = LayoutComponent.Create(name, parent);
			m_Current = Root;
		}

		public LayoutComponent Create(string name, LayoutComponent prefab = null)
		{
			return m_Current.Create(name, prefab);
		}

		public LayoutComponent BeginGroup(string name)
		{
			m_Stack.Push(m_Current);
			m_Current = m_Current.Create(name);
			return m_Current;
		}

		public void EndGroup()
		{
			m_Current = m_Stack.Pop();
		}
	}
}
