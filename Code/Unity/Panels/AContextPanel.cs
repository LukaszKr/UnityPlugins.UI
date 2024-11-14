using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity
{
	public abstract class AContextPanel<TContext> : APanel
	{
		private readonly ContextHandler<TContext> m_ContextHandler;
		protected TContext m_Context;

		public AContextPanel()
		{
			m_ContextHandler = new ContextHandler<TContext>(OnAttach, OnDetach, OnReplace);
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

		protected virtual void OnReplace(TContext context, EventBinder binder, TContext oldContext)
		{
			OnDetach();
			OnAttach(context, binder);
		}
		#endregion
	}
}
