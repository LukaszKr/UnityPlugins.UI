using UnityEngine;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity
{
	[DisallowMultipleComponent]
	public class UINavigationComponent : AUIElementComponent, INavigationReceiver
	{
		[SerializeField]
		protected UIActiveElementComponent m_ActiveElement = null;

		public bool IsNavigationActive => GameObject.activeInHierarchy;

		public CustomEvent<INavigationReceiver> OnNavigationHovered { get; } = new CustomEvent<INavigationReceiver>();

		protected override void OnInitialize(EventBinder binder)
		{
			m_ActiveElement.OnHovered.AddListener(OnHoveredHandler);
		}

		public virtual void NavigationAccepted()
		{
			if(IsNavigationActive)
			{
				m_ActiveElement.Click();
			}
		}

		public virtual void NavigationDeselected()
		{
			m_ActiveElement.TrySetSelected(false);
		}

		public virtual void NavigationSelected()
		{
			m_ActiveElement.TrySetSelected(true);
		}

		public virtual bool Navigate(EGridCardinal2D direction)
		{
			return false;
		}

		#region Callbacks
		private void OnHoveredHandler(bool hovered)
		{
			if(hovered && IsNavigationActive)
			{
				OnNavigationHovered.Invoke(this);
			}
		}
		#endregion

#if UNITY_EDITOR
		private void OnValidate()
		{
			if(m_ActiveElement == null)
			{
				m_ActiveElement = GetComponent<UIActiveElementComponent>();
			}
		}
#endif
	}
}
