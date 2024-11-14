using System.Collections.Generic;
using UnityPlugins.UI.Unity;
using UnityEngine;

namespace UnityPlugins.Gameplay.UI
{
	public class ProceduralUIBuilder
	{
		public readonly RectTransform Root;
		public readonly ProceduralStyleSheet StyleSheet;

		private readonly Stack<UILineLayout> m_Layouts = new Stack<UILineLayout>();

		public ProceduralUIBuilder(RectTransform root, ProceduralStyleSheet styleSheet)
		{
			Root = root;
			StyleSheet = styleSheet;
		}

		#region Layouts
		public void BeginVertical()
		{
			UILineLayout layout = Create(StyleSheet.VerticalLayout);
			m_Layouts.Push(layout);
		}

		public void BeginHorizontal()
		{
			UILineLayout layout = Create(StyleSheet.HorizontalLayout);
			m_Layouts.Push(layout);
		}

		public void EndLayout()
		{
			UILineLayout layout = m_Layouts.Pop();
			layout.AutoPopulate();
			layout.DoLayout();
		}
		#endregion

		#region Labels
		public UILabel Label()
		{
			return Create(StyleSheet.Label);
		}
		#endregion

		#region Buttons
		public UIButton Button()
		{
			return Create(StyleSheet.Button);
		}

		public UITextButton TextButton()
		{
			return Create(StyleSheet.TextButton);
		}

		public UIIconButton IconButton()
		{
			return Create(StyleSheet.IconButton);
		}
		#endregion

		public TElement Create<TElement>(TElement prefab)
			where TElement : Object
		{
			RectTransform parent;
			if(m_Layouts.Count > 0)
			{
				parent = m_Layouts.Peek().RectTransform;
			}
			else
			{
				parent = Root;
			}
			TElement instance = Object.Instantiate(prefab, parent);
			return instance;
		}
	}
}
