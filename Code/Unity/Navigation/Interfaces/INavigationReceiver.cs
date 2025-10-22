using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity
{
	public interface INavigationReceiver
	{
		bool IsNavigationActive { get; }

		void NavigationSelected();
		void NavigationDeselected();
		void NavigationAccepted();
		bool Navigate(EGridCardinal2D direction);

		CustomEvent<INavigationReceiver> OnNavigationSelected { get; }
	}
}
