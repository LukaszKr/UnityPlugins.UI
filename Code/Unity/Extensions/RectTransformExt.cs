using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	public static class RectTransformExt
	{
		public static void SetAnchorX(this RectTransform rect, float value)
		{
			rect.anchorMin = new Vector2(value, rect.anchorMin.y);
			rect.anchorMax = new Vector2(value, rect.anchorMax.y);
		}

		public static void SetAnchorX(this RectTransform rect, float min, float max)
		{
			rect.anchorMin = new Vector2(min, rect.anchorMin.y);
			rect.anchorMax = new Vector2(max, rect.anchorMax.y);
		}

		public static void SetAnchorY(this RectTransform rect, float value)
		{
			rect.anchorMin = new Vector2(rect.anchorMin.x, value);
			rect.anchorMax = new Vector2(rect.anchorMax.x, value);
		}

		public static void SetAnchorY(this RectTransform rect, float min, float max)
		{
			rect.anchorMin = new Vector2(rect.anchorMin.x, min);
			rect.anchorMax = new Vector2(rect.anchorMax.x, max);
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
	}
}
