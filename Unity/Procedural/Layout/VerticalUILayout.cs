using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public class VerticalUILayout : AUILayout
	{
		public bool StretchElementsToFullWidth = true;
		public float Spacing = 4f;

		private float m_Offset = 0;

		public VerticalUILayout(AUILayout parent)
			: base(parent)
		{
			Rect.ExpandWidthAndHeight();
		}

		protected override void ResetLayout()
		{
			m_Offset = 0f;
		}

		protected override void OnAppend(RectTransform element)
		{
			element.SetAnchorY(1f);
			element.ExpandWidth();
			element.pivot = new Vector2(element.pivot.x, 1f);
			element.anchoredPosition = new Vector2(0f, -m_Offset);
			m_Offset += element.rect.height;
			m_Offset += Spacing;
		}
	}
}
