using UnityEngine;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity
{
	public interface INavigationReceiver
	{
		bool IsNavigationActive { get; }
		RectTransform RectTransform { get; }

		void NavigationSelected();
		void NavigationDeselected();
		void NavigationAccepted();

		CustomEvent<INavigationReceiver> OnNavigationSelected { get; }
	}
}
