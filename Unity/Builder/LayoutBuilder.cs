using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public class LayoutBuilder
	{
		private readonly Stack<LayoutComponent> m_Stack = new Stack<LayoutComponent>();
		private LayoutComponent m_CurrentGroup;
		private LayoutComponent m_CurrentLayout;

		public LayoutComponent CurrentGroup => m_CurrentGroup;
		public LayoutComponent CurrentLayout => m_CurrentLayout;

		public LayoutComponent Begin(Transform parent, string name = "Root")
		{
			m_CurrentGroup = LayoutComponent.Create(name, parent);
			m_CurrentLayout = m_CurrentGroup;
			return m_CurrentGroup;
		}

		public void End()
		{
			if(m_Stack.Count > 0)
			{
				throw new NotSupportedException($"{m_Stack.Count} groups are still not closed.");
			}
			m_CurrentGroup = null;
			m_CurrentLayout = null;
		}

		public virtual LayoutComponent Create(string name)
		{
			m_CurrentLayout = m_CurrentGroup.Create(name);
			return m_CurrentLayout;
		}

		public LayoutComponent BeginGroup(string name)
		{
			m_Stack.Push(m_CurrentGroup);
			m_CurrentGroup = Create(name);
			return m_CurrentGroup;
		}

		public void EndGroup()
		{
			m_CurrentGroup = m_Stack.Pop();
		}
	}
}
