using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public readonly struct LayoutInstancePair<TPrefab>
		where TPrefab : Component
	{
		public readonly LayoutComponent Layout;
		public readonly TPrefab Instance;

		public static implicit operator TPrefab(LayoutInstancePair<TPrefab> pair) => pair.Instance;
		public static implicit operator LayoutComponent(LayoutInstancePair<TPrefab> pair) => pair.Layout;

		public LayoutInstancePair(LayoutComponent layout, TPrefab instance)
		{
			Layout = layout;
			Instance = instance;
		}
	}
}
