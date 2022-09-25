using ProceduralLevel.Common.Context;
using ProceduralLevel.Common.Event;
using ProceduralLevel.UnityPlugins.UI.Unity;

namespace ProceduralLevel.UnityPlugins.UI
{
	public abstract class APanelContextElement<TContext> : APanelElement
		where TContext : class
	{
		private ContextClass<TContext> m_Context;

		public TContext Context => m_Context?.Context;

		public void SetContext(TContext newContext)
		{
			if(m_Context == null)
			{
				m_Context = new ContextClass<TContext>(OnAttach, OnDetach, OnReplace);
			}

			m_Context.SetContext(newContext);
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
