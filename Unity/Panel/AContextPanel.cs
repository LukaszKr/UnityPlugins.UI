using ProceduralLevel.Common.Event;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public abstract class AContextPanel<TContext> : APanel
		where TContext : class
	{
		protected TContext m_Context;
		private readonly EventBinder m_ContextBinder = new EventBinder();

		public TContext Context { get { return m_Context; } }

		public void Show(TContext context)
		{
			SetContext(context);
			base.Show();
		}

		protected override void OnHide()
		{
			base.OnHide();
			SetContext(null);
		}

		private void SetContext(TContext newContext)
		{
			if(newContext == m_Context)
			{
				return;
			}

			m_ContextBinder.UnbindAll();
			TContext oldContext = m_Context;
			m_Context = newContext;
			if(newContext != null)
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
			else
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
