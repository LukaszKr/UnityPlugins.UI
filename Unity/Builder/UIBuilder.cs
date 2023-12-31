using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class UIBuilder
	{
		public readonly LayoutComponent Root;

		private readonly Stack<LayoutComponent> m_Stack = new Stack<LayoutComponent>();
		private LayoutComponent m_CurrentGroup;
		private LayoutComponent m_CurrentLayout;

		public LayoutComponent CurrentGroup => m_CurrentGroup;
		public LayoutComponent CurrentLayout => m_CurrentLayout;

		public UIBuilder(Transform parent, string name = "Root")
		{
			Root = LayoutComponent.Create(name, parent);
			m_CurrentGroup = Root;
		}

		public virtual LayoutComponent Create(string name, LayoutComponent prefab = null)
		{
			m_CurrentLayout = m_CurrentGroup.Create(name, prefab);
			return m_CurrentLayout;
		}

		public LayoutComponent BeginGroup(string name, LayoutComponent prefab = null)
		{
			m_Stack.Push(m_CurrentGroup);
			m_CurrentGroup = Create(name, prefab);
			return m_CurrentGroup;
		}

		public void EndGroup()
		{
			m_CurrentGroup = m_Stack.Pop();
		}
	}
}
