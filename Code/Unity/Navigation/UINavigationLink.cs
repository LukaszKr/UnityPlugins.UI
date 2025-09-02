using System.Collections.Generic;

namespace UnityPlugins.UI.Unity
{
	public class UINavigationLink
	{
		private readonly List<UINavigationTarget> m_Targets = new List<UINavigationTarget>();

		public IReadOnlyList<UINavigationTarget> Targets => m_Targets;

		public void Add(UINavigationTarget target)
		{
			m_Targets.Add(target);
		}

		public bool Remove(UINavigationTarget target)
		{
			return m_Targets.Remove(target);
		}

		public UINavigationTarget GetValidTarget(bool allowInactive = false)
		{
			int count = m_Targets.Count;
			if(count == 0)
			{
				return null;
			}

			for(int x = 0; x < count; ++x)
			{
				UINavigationTarget target = m_Targets[x];
				if(!target.Receiver.IsNavigationActive)
				{
					continue;
				}
				return target;
			}

			if(allowInactive)
			{
				return m_Targets[0];
			}
			return null;
		}
	}
}
