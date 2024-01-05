using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public static class RectTransformExt
	{
		public static void SetAnchorX(this RectTransform rect, float value)
		{
			rect.anchorMin = new Vector2(value, rect.anchorMin.y);
			rect.anchorMax = new Vector2(value, rect.anchorMax.y);
		}

		public static void SetAnchorY(this RectTransform rect, float value)
		{
			rect.anchorMin = new Vector2(rect.anchorMin.x, value);
			rect.anchorMax = new Vector2(rect.anchorMax.x, value);
		}

		public static void ExpandWidth(this RectTransform rect)
		{
			rect.anchorMin = new Vector2(0f, rect.anchorMin.y);
			rect.anchorMax = new Vector2(1f, rect.anchorMax.y);
			rect.sizeDelta = new Vector2(0f, rect.sizeDelta.y);
		}

		public static void ExpandHeight(this RectTransform rect)
		{
			rect.anchorMin = new Vector2(rect.anchorMin.x, 0f);
			rect.anchorMax = new Vector2(rect.anchorMax.x, 1f);
			rect.sizeDelta = new Vector2(rect.sizeDelta.x, 0f);
		}

		public static void ExpandWidthAndHeight(this RectTransform rect)
		{
			rect.anchorMin = new Vector2(0f, 0f);
			rect.anchorMax = new Vector2(1f, 1f);
			rect.sizeDelta = new Vector2(0f, 0f);
		}

		public static void ApplyLayout(this RectTransform rect, Layout layout)
		{
			rect.anchorMin = new Vector2(0f, 1f);
			rect.anchorMax = new Vector2(0f, 1f);
			Vector2 pivot = rect.pivot;
			LayoutRect layoutRect = layout.Rect;
			Vector2 position = new Vector2(layoutRect.X, -layoutRect.Y);
			position.x += pivot.x*layoutRect.Width;
			position.y -= pivot.y*layoutRect.Height;
			rect.anchoredPosition = position;
			rect.sizeDelta = new Vector2(layoutRect.Width, layoutRect.Height);
		}
	}
}
