using ProceduralLevel.Common.Event;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public class ImmediateUI
	{
		public readonly RectTransform Container;
		public readonly ImmediateUIConfig Config;

		private readonly EventBinder m_Binder = new EventBinder();
		private readonly Stack<AUILayout> m_LayoutStack = new Stack<AUILayout>();
		private AUILayout m_CurrentLayout;

		public ImmediateUI(RectTransform container, ImmediateUIConfig config)
		{
			Container = container;
			Config = config;
			m_CurrentLayout = new VerticalUILayout(null);
			m_CurrentLayout.Rect.SetParent(container, false);
		}

		#region Elements
		public void AppendElement(RectTransform transform)
		{
			m_CurrentLayout.Append(transform);
		}

		public TElement CreateElement<TElement>(TElement elementPrefab)
			where TElement : AElement
		{
			TElement instance = Object.Instantiate(elementPrefab);
			AppendElement(instance.RectTransform);
			return instance;
		}
		#endregion

		#region Layout
		public void BeginLayout(AUILayout layout)
		{
			if(m_CurrentLayout != null)
			{
				m_CurrentLayout.Append(layout);
			}
			m_LayoutStack.Push(layout);
			m_CurrentLayout = layout;
		}

		public VerticalUILayout BeginVertical()
		{
			VerticalUILayout layout = new VerticalUILayout(m_CurrentLayout);
			BeginLayout(layout);
			return layout;
		}

		public void EndLayout()
		{
			m_CurrentLayout = m_LayoutStack.Pop();
		}
		#endregion

		#region Label
		public UIText Label()
		{
			return CreateElement(Config.Label);
		}

		public UIText Label(string text)
		{
			UIText label = Label();
			label.SetText(text);
			return label;
		}
		#endregion

		#region Button
		public UITextButton TextButton()
		{
			return CreateElement(Config.TextButton);
		}

		public UITextButton TextButton(string text, AEvent.Callback onClicked)
		{
			UITextButton button = TextButton();
			button.Text.SetText(text);
			m_Binder.Bind(button.OnClicked, onClicked);
			return button;
		}

		public UIIconButton IconButton()
		{
			return CreateElement(Config.IconButon);
		}

		public UIIconButton IconButton(Sprite sprite, AEvent.Callback onClicked)
		{
			UIIconButton button = IconButton();
			button.Icon.sprite = sprite;
			m_Binder.Bind(button.OnClicked, onClicked);
			return button;
		}
		#endregion

		#region Toggle
		public UIToggle Toggle()
		{
			return CreateElement(Config.Toggle);
		}

		public UIToggle Toggle(AEvent<bool>.Callback onValueChanged)
		{
			UIToggle toggle = Toggle();
			m_Binder.Bind(toggle.OnValueChanged, onValueChanged);
			return toggle;
		}

		public UILabelToggle LabelToggle()
		{
			return CreateElement(Config.LabelToggle);
		}

		public UILabelToggle LabelToggle(string label, AEvent<bool>.Callback onValueChanged)
		{
			UILabelToggle toggle = LabelToggle();
			m_Binder.Bind(toggle.OnValueChanged, onValueChanged);
			return toggle;
		}
		#endregion
	}
}
