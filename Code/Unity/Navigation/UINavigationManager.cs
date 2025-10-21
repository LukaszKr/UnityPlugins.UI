using System.Collections.Generic;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity
{
	public class UINavigationManager
	{
		private List<UINavigator> m_NavigatorsStack = new List<UINavigator>();

		private UINavigator m_CurrentNavigator;

		public void PushNavigator(UINavigator navigator)
		{
			GameAssert.IsNotNull(navigator);
			GameAssert.AreNotEqual(m_CurrentNavigator, navigator);
			GameAssert.IsFalse(m_NavigatorsStack.Contains(navigator));

			if(m_CurrentNavigator != null)
			{
				m_NavigatorsStack.Add(m_CurrentNavigator);
			}
			m_CurrentNavigator = navigator;
			navigator.Select();
		}

		public void PopNavigator(UINavigator navigator)
		{
			GameAssert.IsNotNull(navigator);

			if(m_CurrentNavigator != navigator)
			{
				bool removed = m_NavigatorsStack.Remove(navigator);
				GameAssert.IsTrue(removed);
				return;
			}

			m_CurrentNavigator.Deselect();

			int lastIndex = m_NavigatorsStack.Count-1;
			if(lastIndex >= 0)
			{
				m_CurrentNavigator = m_NavigatorsStack[lastIndex];
				m_NavigatorsStack.RemoveAt(lastIndex);
				return;
			}

			m_CurrentNavigator = null;
		}

		public void Navigate(EGridCardinal2D direction)
		{
			if(m_CurrentNavigator == null)
			{
				return;
			}
			m_CurrentNavigator.Navigate(direction);
		}

		public void Accept()
		{
			if(m_CurrentNavigator == null)
			{
				return;
			}
			m_CurrentNavigator.AcceptSelected();
		}
	}
}
