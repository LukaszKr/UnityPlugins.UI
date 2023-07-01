using ProceduralLevel.Common.Event;
using ProceduralLevel.Common.Unity.Extended;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
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
			m_RectTransform = GetComponent<RectTransform>();
		}

		private void OnDestroy()
		{
			if(m_IsInitialized)
			{
				m_IsInitialized = false;
				m_ElementBinder.UnbindAll();
				OnCleanup();
			}
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
				OnInitialize(m_ElementBinder);
			}
		}

		protected abstract void OnInitialize(EventBinder binder);

		protected virtual void OnCleanup()
		{
		}
	}
}
