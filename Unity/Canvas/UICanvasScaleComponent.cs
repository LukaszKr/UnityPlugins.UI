using UnityEngine;
using UnityPlugins.Common.Logic;
using UnityPlugins.Common.Unity;

namespace UnityPlugins.UI.Unity
{
	[ExecuteAlways]
	public class UICanvasScaleComponent : ExtendedMonoBehaviour
	{
		private readonly EventBinder m_Binder = new EventBinder();

		private float m_ScaleModifier = 1f;
		private int m_LastWidth = 0;
		private int m_LastHeight = 0;
		private float m_LastScaleModifier;

		public Canvas Canvas = null;
		public int ReferenceWidth = 1920;

		public float MinAspect = 1.5f;
		public float MaxAspect = 2f;

		private void OnDisable()
		{
			m_Binder.UnbindAll();
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
			if(!Canvas)
			{
				return;
			}

			int rawWidth = Screen.width;
			int rawHeight = Screen.height;

			if(rawWidth == m_LastWidth && rawHeight == m_LastHeight && Mathf.Approximately(m_LastScaleModifier, m_ScaleModifier))
			{
				return;
			}

			m_LastWidth = rawWidth;
			m_LastHeight = rawHeight;
			m_LastScaleModifier = m_ScaleModifier;

			float scale = 1f;
			float width = rawWidth * m_ScaleModifier;
			float height = rawHeight * m_ScaleModifier;

			float aspect = width/(float)height;
			if(aspect < MinAspect)
			{
				scale = width/ReferenceWidth;
			}
			else if(aspect > MaxAspect)
			{
				scale = (height*MaxAspect)/ReferenceWidth;
			}
			else
			{
				scale = width/ReferenceWidth;
			}

			Canvas.scaleFactor = scale;
		}

		public void SetScaleModifier(float value)
		{
			if(Mathf.Approximately(m_ScaleModifier, value))
			{
				return;
			}

			m_ScaleModifier = value;
		}
	}
}
