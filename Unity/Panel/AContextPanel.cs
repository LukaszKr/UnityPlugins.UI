using ProceduralLevel.Common.Context;
using ProceduralLevel.Common.Event;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public abstract class AContextPanel<TContext> : APanel
		where TContext : class
	{
		private ContextClass<TContext> m_Context;

		public TContext Context { get { return m_Context?.Context; } }

		protected override void Awake()
		{
			base.Awake();
		}

		public void Show(TContext context)
		{
			base.Show();
			SetContext(context);
		}

		protected override void OnHide()
		{
			base.OnHide();
			SetContext(null);
		}

		private void SetContext(TContext newContext)
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
