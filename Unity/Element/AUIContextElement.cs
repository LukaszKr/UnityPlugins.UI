using System;
using System.Collections.Generic;
using ProceduralLevel.Common.Event;

namespace ProceduralLevel.UI.Unity
{
	public abstract class AUIContextElement<TContext> : AUIElement
		where TContext : class
	{
		protected TContext m_Context;
		private readonly EventBinder m_ContextBinder = new EventBinder();

		private bool m_Initialized;
		private bool m_ContextIsSet;

		public void ClearContext()
		{
			if(m_ContextIsSet)
			{
				Detach();
			}
		}

		public void SetContext(TContext context)
		{
			if(!m_Initialized)
			{
				TryInitialize();
				m_Initialized = true;
			}

			if(EqualityComparer<TContext>.Default.Equals(context, m_Context))
			{
				throw new InvalidOperationException();
			}

			if(m_ContextIsSet)
			{
				Replace(m_Context);
			}
			else
			{
				Attach(context);
			}

			m_ContextBinder.UnbindAll();
		}

		private void Attach(TContext context)
		{
			m_ContextIsSet = true;
			m_ContextBinder.UnbindAll();
			m_Context = context;
			OnAttach(m_ContextBinder);
		}

		private void Replace(TContext context)
		{
			m_ContextBinder.UnbindAll();
			TContext oldContext = m_Context;
			m_Context = context;
			OnReplace(m_ContextBinder, oldContext);
		}

		private void Detach()
		{
			m_ContextIsSet = false;
			m_Context = default;
			m_ContextBinder.UnbindAll();
			OnDetach();
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
