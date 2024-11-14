using UnityPlugins.Common.Logic;
using UnityPlugins.Common.Unity;
using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	public abstract class AUIElement : ExtendedMonoBehaviour
	{
		private RectTransform m_RectTransform;

		private bool m_IsInitialized;
		private readonly EventBinder m_ElementBinder = new EventBinder();

		public RectTransform RectTransform => m_RectTransform;

		#region Unity
		protected virtual void Awake()
		{
			TryInitialize();
		}

		protected virtual void OnDestroy()
		{
			m_ElementBinder.UnbindAll();
		}

		protected virtual void OnEnable()
		{
			m_ElementBinder.Enable();
		}

		protected virtual void OnDisable()
		{
			m_ElementBinder.Disable();
		}
		#endregion

		protected void TryInitialize()
		{
			if(!m_IsInitialized)
			{
				m_IsInitialized = true;
				m_RectTransform = GetComponent<RectTransform>();
				OnInitialize(m_ElementBinder);
			}
		}

		protected abstract void OnInitialize(EventBinder binder);
	}
}
