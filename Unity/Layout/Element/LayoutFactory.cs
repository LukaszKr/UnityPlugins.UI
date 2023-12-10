using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public static class LayoutFactory
	{
		public static LayoutComponent Create(Layout layout, Transform parent, string name, LayoutComponent prefab = null)
		{
			LayoutComponent component;
			if(prefab == null)
			{
				GameObject go = new GameObject(name);
				go.transform.SetParent(parent, false);
				_ = go.AddComponent<RectTransform>();
				component = go.AddComponent<LayoutComponent>();
			}
			else
			{
				component = Object.Instantiate(prefab, parent, false);
				component.name = name;
			}
			component.Setup(layout);
			return component;
		}
	}
}
