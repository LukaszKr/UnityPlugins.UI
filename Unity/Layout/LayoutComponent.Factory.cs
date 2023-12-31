using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public partial class LayoutComponent
	{
		#region Create
		public static LayoutComponent Create(string name, Transform parent, LayoutComponent prefab = null)
		{
			Layout layout = new Layout(null);
			return Create(name, parent, layout, prefab);
		}

		private static LayoutComponent Create(string name, Transform parent, Layout layout, LayoutComponent prefab = null)
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
				component = Instantiate(prefab, parent, false);
				component.name = name;
			}
			component.Setup(layout);
			return component;
		}

		public LayoutComponent Create(string name, LayoutComponent prefab = null)
		{
			return Create(name, Transform, m_Layout.CreateChild(), prefab);
		}
		#endregion
	}
}
