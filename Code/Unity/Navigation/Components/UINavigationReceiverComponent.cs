using UnityEngine;
using UnityPlugins.Common.Logic;
using UnityPlugins.Common.Unity;

namespace UnityPlugins.UI.Unity
{
	[DisallowMultipleComponent]
	public class UINavigationReceiverComponent : ExtendedMonoBehaviour, INavigationReceiver
	{
		[SerializeField]
		private UIActiveElementComponent m_ActiveElement = null;

		public bool IsNavigationActive => GameObject.activeInHierarchy;

		public CustomEvent<INavigationReceiver> OnNavigationSelected { get; private set; } = new CustomEvent<INavigationReceiver>();

		private void Awake()
		{
			m_ActiveElement.OnHovered.AddListener(OnHoveredHandler);
		}

		public void NavigationAccepted()
		{
			if(IsNavigationActive)
			{
				m_ActiveElement.Click();
			}
		}	

		public void NavigationDeselected()
		{
			m_ActiveElement.TrySetHovered(false);
		}

		public void NavigationSelected()
		{
			m_ActiveElement.TrySetHovered(true);
		}

		#region Callbacks
		private void OnHoveredHandler(bool hovered)
		{
			if(hovered && IsNavigationActive)
			{
				OnNavigationSelected.Invoke(this);
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
