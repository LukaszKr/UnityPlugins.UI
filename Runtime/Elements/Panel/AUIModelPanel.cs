using ProceduralLevel.Common.Event;

namespace ProceduralLevel.UnityPlugins.UI
{
	public abstract class AUIModelPanel<TModel> : AUIPanel
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
					OnReplaceModel(m_ModelBinder, oldModel);
				}
				else
				{
					OnAttachModel(m_ModelBinder);
				}
			}
			else
			{
				OnDetachModel();
			}
		}

		protected virtual void OnReplaceModel(EventBinder binder, TModel oldModel)
		{
			OnDetachModel();
			OnAttachModel(binder);
		}

		protected abstract void OnAttachModel(EventBinder binder);
		protected abstract void OnDetachModel();
	}
}
