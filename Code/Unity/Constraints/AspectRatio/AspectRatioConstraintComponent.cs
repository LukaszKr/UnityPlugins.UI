using UnityPlugins.Common.Unity;
using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	public class AspectRatioConstraintComponent : ExtendedMonoBehaviour
	{
		private int m_KnownWidth;
		private int m_KnownHeight;

		private RectTransform m_Target = null;
		private Canvas m_Canvas = null;

		[SerializeField]
		private AspectRatioConstraintSO m_Settings = null;

		private void Awake()
		{
			m_Target = GetComponent<RectTransform>();
			m_Canvas = GetComponentInParent<Canvas>();
		}

		private void OnEnable()
		{
			Refresh();
		}

		private void Update()
		{
			Refresh();
		}

		private void Refresh()
		{
			int width = Screen.width;
			int height = Screen.height;
			if(m_KnownWidth != width || m_KnownHeight != height)
			{
				Recalculate(width, height);
			}
		}

		private void Recalculate(int width, int height)
		{
			m_KnownWidth = width;
			m_KnownHeight = height;

			float aspect = width/(float)height;

			float minAspect = m_Settings.MinAspect;
			float maxAspect = m_Settings.MaxAspect;

			if(aspect < minAspect)
			{
				int maxHeight = (int)(width/minAspect);
				SetSize(width, maxHeight);

			}
			else if(aspect > maxAspect)
			{
				int maxWidth = (int)(height*maxAspect);
				SetSize(maxWidth, height);
			}
			else
			{
				SetSize(width, height);
			}
		}

		private void SetSize(int width, int height)
		{
			float scale = m_Canvas.scaleFactor;
			m_Target.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width/scale);
			m_Target.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height/scale);
		}
	}
}
