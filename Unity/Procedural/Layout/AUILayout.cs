using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public abstract class AUILayout
	{
		public readonly AUILayout Parent;
		public readonly RectTransform Rect;
		
		protected readonly List<RectTransform> m_Elements = new List<RectTransform>();
		protected readonly List<AUILayout> m_Layouts = new List<AUILayout>();

		protected AUILayout(AUILayout parent)
		{
			Parent = parent;
			GameObject go = new GameObject(GetType().Name);
			Rect = go.AddComponent<RectTransform>();
			if(parent != null)
			{
				go.transform.SetParent(parent?.Rect, false);
			}
		}

		public void Append(RectTransform element)
		{
			m_Elements.Add(element);
			element.SetParent(Rect, false);
			OnAppend(element);
		}
		protected abstract void OnAppend(RectTransform element);

		public void Append(AUILayout layout)
		{
			m_Layouts.Add(layout);
			Append(layout.Rect);
		}

		#region Layout
		public void RebuildLayout()
		{
			ResetLayout();
			int count = m_Elements.Count;
			for(int x = 0; x < count; ++x)
			{
				OnAppend(m_Elements[x]);
			}
		}

		protected abstract void ResetLayout();
		#endregion
	}
}
