using ProceduralLevel.Common.Event;
using ProceduralLevel.UnityPlugins.Common.Extended;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public abstract class AUIElement: ExtendedMonoBehaviour
	{
		private RectTransform m_RectTransform;

		private bool m_IsPrepared;
		protected readonly EventBinder m_Binder = new EventBinder();

		public RectTransform RectTransform { get { return m_RectTransform; } }

		#region Unity
		protected virtual void Awake()
		{
			m_RectTransform = GetComponent<RectTransform>();
		}

		private void OnDestroy()
		{
			if(m_IsPrepared)
			{
				m_IsPrepared = false;
				m_Binder.UnbindAll();
				OnCleanup();
			}
		}

		private void OnEnable()
		{
			m_Binder.Enable();
		}

		private void OnDisable()
		{
			m_Binder.Disable();
		}
		#endregion

		protected void TryPrepare()
		{
			if(!m_IsPrepared)
			{
				m_IsPrepared = true;
				OnPrepare(m_Binder);
			}
		}

		protected abstract void OnPrepare(EventBinder binder);
		protected abstract void OnCleanup();
	}
}
