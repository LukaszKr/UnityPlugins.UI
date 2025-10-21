using System.Collections.Generic;

namespace UnityPlugins.UI.Unity
{
	public static class UINavigatorExt
	{
		public static UINavigationTarget Add(this UINavigator navigator, INavigationReceiverComponent target)
		{
			return navigator.Add(target.Navigation);
		}

		public static UINavigationTarget[] Add(this UINavigator navigator, params INavigationReceiverComponent[] targets)
		{
			UINavigationTarget[] results = new UINavigationTarget[targets.Length];
			for(int x = 0; x < targets.Length; x++)
			{
				INavigationReceiverComponent target = targets[x];
				UINavigationTarget uiTarget = navigator.Add(target.Navigation);
				results[x] = uiTarget;
			}
			return results;
		}

		public static void Add(this UINavigator navigator, List<UINavigationTarget> targets, INavigationReceiverComponent receiver)
		{
			UINavigationTarget result = navigator.Add(receiver.Navigation);
			targets.Add(result);
		}

		public static void Add(this UINavigator navigator, List<UINavigationTarget> targets, params INavigationReceiverComponent[] receivers)
		{
			for(int x = 0; x < receivers.Length; x++)
			{
				INavigationReceiverComponent receiver = receivers[x];
				navigator.Add(targets, receiver.Navigation);
			}
		}
	}
}
