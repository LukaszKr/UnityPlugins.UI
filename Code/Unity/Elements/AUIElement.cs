using System;
using UnityEngine;
using UnityPlugins.Common.Logic;
using UnityPlugins.Common.Unity;

namespace UnityPlugins.UI.Unity
{
	public abstract class AUIElement : ExtendedMonoBehaviour
	{
		private bool m_IsInitialized;
		private readonly EventBinder m_ElementBinder = new EventBinder();

		[NonSerialized]
		private RectTransform m_RectTransform;

		public RectTransform RectTransform
		{
			get
			{
				if(ReferenceEquals(m_RectTransform, null))
				{
					m_RectTransform = GetComponent<RectTransform>();
				}
				return m_RectTransform;
			}
		}

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
				OnInitialize(m_ElementBinder);
			}
		}

		protected abstract void OnInitialize(EventBinder binder);
	}
}
