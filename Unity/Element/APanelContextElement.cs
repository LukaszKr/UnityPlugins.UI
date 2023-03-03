﻿using ProceduralLevel.Common.Event;
using ProceduralLevel.UnityPlugins.UI.Unity;
using System;

namespace ProceduralLevel.UnityPlugins.UI
{
	public abstract class APanelContextElement<TContext> : APanelElement
		where TContext : class
	{
		protected TContext m_Context;
		private readonly EventBinder m_ContextBinder = new EventBinder();

		public void SetContext(TContext context)
		{
			TryInitialize();
			if(context == m_Context)
			{
				throw new InvalidOperationException();
			}

			m_ContextBinder.UnbindAll();
			TContext oldContext = m_Context;
			m_Context = context;
			if(context != null)
			{
				if(oldContext != null)
				{
					OnReplace(m_ContextBinder, oldContext);
				}
				else
				{
					OnAttach(m_ContextBinder);
				}
			}
			else if(oldContext != null)
			{
				OnDetach();
			}
		}

		protected virtual void OnReplace(EventBinder binder, TContext oldContext)
		{
			OnDetach();
			OnAttach(binder);
		}

		protected abstract void OnAttach(EventBinder binder);
		protected abstract void OnDetach();
	}
}
