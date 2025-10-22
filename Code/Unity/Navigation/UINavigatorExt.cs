using System.Collections.Generic;

namespace UnityPlugins.UI.Unity
{
	public static class UINavigatorExt
	{
		public static UINavigationTarget Add(this UINavigator navigator, INavigationReceiverProvider target)
		{
			return navigator.Add(target.Navigation);
		}

		public static UINavigationTarget[] Add(this UINavigator navigator, params INavigationReceiverProvider[] targets)
		{
			UINavigationTarget[] results = new UINavigationTarget[targets.Length];
			for(int x = 0; x < targets.Length; x++)
			{
				INavigationReceiverProvider target = targets[x];
				UINavigationTarget uiTarget = navigator.Add(target.Navigation);
				results[x] = uiTarget;
			}
			return results;
		}

		public static void Add(this UINavigator navigator, List<UINavigationTarget> targets, INavigationReceiverProvider receiver)
		{
			UINavigationTarget result = navigator.Add(receiver.Navigation);
			targets.Add(result);
		}

		public static void Add(this UINavigator navigator, List<UINavigationTarget> targets, params INavigationReceiverProvider[] receivers)
		{
			for(int x = 0; x < receivers.Length; x++)
			{
				INavigationReceiverProvider receiver = receivers[x];
				navigator.Add(targets, receiver.Navigation);
			}
		}
	}
}
