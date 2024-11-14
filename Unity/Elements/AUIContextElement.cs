using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity
{
	public abstract class AUIContextElement<TContext> : AUIElement
		where TContext : class
	{
		private readonly ContextHandler<TContext> m_ContextHandler;
		protected TContext m_Context;

		private readonly EventBinder m_ContextBinder = new EventBinder();

		public AUIContextElement()
		{
			m_ContextHandler = new ContextHandler<TContext>(OnAttach, OnDetach, OnReplace);
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

		protected abstract void OnInitialize();
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
