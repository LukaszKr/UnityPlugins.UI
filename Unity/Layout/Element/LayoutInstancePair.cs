using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public readonly struct LayoutInstancePair<TPrefab>
		where TPrefab : Component
	{
		public readonly LayoutComponent Component;
		public readonly TPrefab Instance;

		public static implicit operator TPrefab(LayoutInstancePair<TPrefab> pair) => pair.Instance;
		public static implicit operator LayoutComponent(LayoutInstancePair<TPrefab> pair) => pair.Component;

		public LayoutInstancePair(LayoutComponent component, TPrefab instance)
		{
			Component = component;
			Instance = instance;
		}
	}
}
