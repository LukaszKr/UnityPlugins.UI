﻿using ProceduralLevel.Common.Event;
using ProceduralLevel.UnityPlugins.Common.Unity.Extended;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public abstract class AElement : ExtendedMonoBehaviour
	{
		private RectTransform m_RectTransform;

		private bool m_IsPrepared;
		private readonly EventBinder m_ElementBinder = new EventBinder();

		public RectTransform RectTransform => m_RectTransform;

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

		protected void TryPrepare()
		{
			if(!m_IsPrepared)
			{
				m_IsPrepared = true;
				OnPrepare(m_ElementBinder);
			}
		}

		protected abstract void OnPrepare(EventBinder binder);

		protected virtual void OnCleanup()
		{
		}
	}
}
