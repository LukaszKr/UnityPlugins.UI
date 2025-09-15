using System;
using System.Collections.Generic;
using UnityEngine;
using UnityPlugins.Common.Unity;

namespace UnityPlugins.UI.Unity
{
	public class UILineLayoutComponent : ExtendedMonoBehaviour
	{
		public RectTransform RectToControl = null;
		[HideInInspector]
		public ELayoutAxis Axis = ELayoutAxis.Vertical;
		[HideInInspector]
		public bool ExpandMainAxis = false;
		[HideInInspector]
		public bool ExpandOtherAxis = false;
		[Header("Spacing")]
		public int m_Spacing = 20;
		public int m_PaddingBefore = 0;
		public int PaddingAfter = 0;

		private readonly List<RectTransform> m_Targets = new List<RectTransform>();

		private bool IsTargetValid(RectTransform target)
		{
			return target.gameObject.activeSelf;
		}

		public void DoLayout()
		{
			int count = m_Targets.Count;

			int activeCount = 0;
			for(int x = 0; x < count; ++x)
			{
				RectTransform target = m_Targets[x];
				if(!IsTargetValid(target))
				{
					continue;
				}
				activeCount++;
			}

			float perSlotSize = 1f/activeCount;

			int totalSize = m_PaddingBefore;
			for(int x = 0; x < count; ++x)
			{
				RectTransform target = m_Targets[x];
				if(!IsTargetValid(target))
				{
					continue;
				}
				if(x > 0)
				{
					totalSize += m_Spacing;
				}

				Rect rect = target.rect;
				int size;
				Vector2 pivot = target.pivot;

				float minAnchor = x*perSlotSize;
				float maxAnchor = (x+1)*perSlotSize;

				switch(Axis)
				{
					case ELayoutAxis.Vertical:
						if(ExpandOtherAxis)
						{
							Vector2 sizeDelta = target.sizeDelta;
							sizeDelta.x = 0f;
							target.sizeDelta = sizeDelta;
							target.anchorMin = new Vector2(0f, 1f);
							target.anchorMax = new Vector2(1f, 1f);
						}
						else
						{
							target.anchorMin = new Vector2(0.5f, 1f);
							target.anchorMax = new Vector2(0.5f, 1f);
						}
						size = (int)target.rect.height;

						target.anchoredPosition = new Vector3(0f, -totalSize - (1f-pivot.y)*size, 0f);
						break;

					case ELayoutAxis.Horizontal:
						if(ExpandOtherAxis)
						{
							Vector2 sizeDelta = target.sizeDelta;
							sizeDelta.y = 0f;
							target.sizeDelta = sizeDelta;
							target.anchorMin = new Vector2(0f, 0f);
							target.anchorMax = new Vector2(0f, 1f);
						}
						else
						{
							target.anchorMin = new Vector2(0f, 0.5f);
							target.anchorMax = new Vector2(0f, 0.5f);
						}

						if(ExpandMainAxis)
						{
							target.SetAnchorX(minAnchor, maxAnchor);
							Vector2 sizeDelta = target.sizeDelta;
							sizeDelta.x = -m_Spacing;
							target.sizeDelta = sizeDelta;
						}
						size = (int)target.rect.width;

						if(ExpandMainAxis)
						{
							target.anchoredPosition = new Vector3(0f, 0f, 0f);
						}
						else
						{
							target.anchoredPosition = new Vector3(totalSize + pivot.x*size, 0f, 0f);
						}
						break;

					default:
						throw new NotImplementedException(Axis.ToString());
				}

				totalSize += size;
			}

			totalSize += PaddingAfter;

			switch(Axis)
			{
				case ELayoutAxis.Vertical:
					RectToControl.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, totalSize);
					break;
				case ELayoutAxis.Horizontal:
					RectToControl.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalSize);
					break;
				default:
					throw new NotImplementedException(Axis.ToString());
			}
		}

		public void AutoPopulate()
		{
			Clear();
			int childCount = RectToControl.childCount;
			for(int x = 0; x < childCount; ++x)
			{
				Transform child = RectToControl.GetChild(x);
				Add(child.gameObject);
			}
		}

		public void Add(GameObject go)
		{
			Add(go.GetComponent<RectTransform>());
		}

		public void Add(RectTransform target)
		{
			m_Targets.Add(target);
		}

		public void Clear()
		{
			m_Targets.Clear();
		}

		[ContextMenu("DoLayout")]
		public void Editor_DoLayout()
		{
			AutoPopulate();
			DoLayout();
			Clear();
#if UNITY_EDITOR
			UnityEditor.EditorUtility.SetDirty(this);
#endif
		}
	}
}
