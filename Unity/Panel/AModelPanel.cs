using ProceduralLevel.Common.Event;

namespace ProceduralLevel.UnityPlugins.UI.Unity
{
	public abstract class AModelPanel<TModel> : APanel
		where TModel : class
	{
		protected TModel m_Model;
		private readonly EventBinder m_ModelBinder = new EventBinder();

		public TModel Model { get { return m_Model; } }

		public void Show(TModel model)
		{
			SetModel(model);
			base.Show();
		}

		protected override void OnHide()
		{
			base.OnHide();
			SetModel(null);
		}

		public virtual void SetModel(TModel newModel)
		{
			if(newModel == m_Model)
			{
				return;
			}

			m_ModelBinder.UnbindAll();
			TModel oldModel = m_Model;
			m_Model = newModel;
			if(newModel != null)
			{
				if(oldModel != null)
				{
					OnReplace(m_ModelBinder, oldModel);
				}
				else
				{
					OnAttach(m_ModelBinder);
				}
			}
			else
			{
				OnDetach();
			}
		}

		protected virtual void OnReplace(EventBinder binder, TModel oldModel)
		{
			OnDetach();
			OnAttach(binder);
		}

		protected abstract void OnAttach(EventBinder binder);
		protected abstract void OnDetach();
	}
}
