using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity
{
	public abstract class AContextPanel<TContext> : APanel
	{
		private readonly ContextHandler<TContext> m_ContextHandler;
		protected TContext m_Context;

		public AContextPanel()
		{
			m_ContextHandler = new ContextHandler<TContext>(OnAttach, OnDetach, Replace);
		}

		public void Show(TContext context)
		{
			if(!IsShown)
			{
				Show();
			}
			m_ContextHandler.SetContext(context);
		}

		protected override void OnHide()
		{
			base.OnHide();
			m_ContextHandler.ClearContext();
		}

		#region Context
		private void OnAttach(TContext context, EventBinder binder)
		{
			m_Context = context;
			OnAttach(binder);
		}

		protected abstract void OnAttach(EventBinder binder);
		protected abstract void OnDetach();

		private void Replace(TContext newContext, EventBinder binder, TContext oldContext)
		{
			m_Context = newContext;
			OnReplace(binder, oldContext);
		}

		protected virtual void OnReplace(EventBinder binder, TContext oldContext)
		{
			OnDetach();
			OnAttach(m_Context, binder);
		}
		#endregion
	}
}
