using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity
{
	public abstract class AUIContextElement<TContext> : AUIElement
	{
		private readonly ContextHandler<TContext> m_ContextHandler;
		protected TContext m_Context;

		private readonly EventBinder m_ContextBinder = new EventBinder();

		public AUIContextElement()
		{
			m_ContextHandler = new ContextHandler<TContext>(OnAttach, OnDetach, Replace);
		}

		#region Context
		public void SetContext(TContext context)
		{
			TryInitialize();

			m_ContextHandler.SetContext(context);
		}

		public void ClearContext()
		{
			m_ContextHandler.ClearContext();
		}

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
