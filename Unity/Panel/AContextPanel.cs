using System;
using ProceduralLevel.Common.Event;

namespace ProceduralLevel.UI.Unity
{
	public abstract class AContextPanel<TContext> : APanel
		where TContext : class
	{
		protected TContext m_Context;
		private readonly EventBinder m_ContextBinder = new EventBinder();

		protected override void Awake()
		{
			base.Awake();
		}

		public void Show(TContext context)
		{
			if(!IsShown)
			{
				Show();
			}
			SetContext(context);
		}

		protected override void OnHide()
		{
			base.OnHide();
			SetContext(null);
		}

		#region Context
		public void SetContext(TContext context)
		{
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
		#endregion
	}
}
