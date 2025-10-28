using UnityEngine;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity.Navigation
{
	public class TestNavigationReceiver : INavigationReceiver
	{
		public string Name;
		public int AcceptedCount;
		public int SelectedCount;
		public int DeselectedCount;

		public bool IsNavigationActive { get; set; }

		public CustomEvent<INavigationReceiver> OnNavigationHovered { get; private set; } = new CustomEvent<INavigationReceiver>();
		public RectTransform RectTransform => null;

		public TestNavigationReceiver(bool active = true, string name = "")
		{
			IsNavigationActive = active;
			Name = name;
		}

		public void NavigationSelected()
		{
			SelectedCount++;
		}

		public void NavigationDeselected()
		{
			DeselectedCount++;
		}

		public void NavigationAccepted()
		{
			AcceptedCount++;
		}

		public bool Navigate(EGridCardinal2D direction)
		{
			return false;
		}

		public override string ToString()
		{
			return $"[{GetType().Name}, {IsNavigationActive}, {Name}]";
		}
	}
}
