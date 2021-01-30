namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public abstract class AUIPanel: AUIElement
	{
		private UICanvas m_Canvas;
		private PanelManager m_Manager;

		private bool m_IsShown = false;

		public UICanvas Canvas { get { return m_Canvas; } }
		public PanelManager Manager { get { return m_Manager; } }

		internal void Setup(UICanvas canvas, PanelManager manager)
		{
			m_Canvas = canvas;
			m_Manager = manager;

			TryPrepare();
		}

		protected override void OnCleanup()
		{
		}

		public void Show()
		{
			if(!m_IsShown && CanShow())
			{
				m_IsShown = true;
				m_Canvas.GameObject.SetActive(true);
				m_Manager.Add(this, m_Canvas);
				OnShow();
			}
		}

		public void Hide()
		{
			if(m_IsShown)
			{
				m_IsShown = false;
				OnHide();
				m_Canvas.GameObject.SetActive(false);
				m_Manager.Remove(this);
			}
		}

		protected virtual bool CanShow()
		{
			return true;
		}

		protected virtual void OnShow()
		{
		}

		protected virtual void OnHide()
		{
		}
	}
}
