using UnityPlugins.Common.Logic;
using UnityPlugins.Common.Unity;

namespace UnityPlugins.UI.Unity
{
	public class UICanvasScaleControllerComponent : ExtendedMonoBehaviour
	{
		public static readonly Observable<int> Scale = new Observable<int>(100);

		private readonly EventBinder m_Binder = new EventBinder();

		private int m_CurrentScale = 0;
		private bool m_ScaleIsDirty = false;

		public UICanvasScaleComponent Target = null;

		private void OnEnable()
		{
			m_Binder.Bind(Scale.OnChanged, OnScaleChangedHandler);
			SetScale(Scale.Value);
		}

		private void OnDisable()
		{
			m_Binder.UnbindAll();
		}

		private void Update()
		{
			if(m_ScaleIsDirty)
			{
				m_ScaleIsDirty = false;
				SetScale(Scale.Value);
			}
		}

		private void SetScale(int scale)
		{
			if(m_CurrentScale == scale)
			{
				return;
			}

			m_CurrentScale = scale;

			float floatScale = scale/100f;
			Target.SetScaleModifier(floatScale);
		}

		#region Callbacks
		private void OnScaleChangedHandler(int value)
		{
			m_ScaleIsDirty = true;
		}
		#endregion
	}
}
