using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public readonly struct LayoutInstancePair<TPrefab>
		where TPrefab : Component
	{
		public readonly LayoutComponent Layout;
		public readonly TPrefab Instance;

		public LayoutInstancePair(LayoutComponent layout, TPrefab instance)
		{
			Layout = layout;
			Instance = instance;
		}
	}
}
