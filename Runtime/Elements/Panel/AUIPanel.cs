namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public abstract class AUIPanel: AUIElement
	{
		private UICanvas m_Canvas;
		private PanelManager m_PanelManager;

		private bool m_IsShown = false;

		public UICanvas Canvas { get { return m_Canvas; } }
		public PanelManager PanelManager { get { return m_PanelManager; } }

		internal void Setup(UICanvas canvas, PanelManager panelManager)
		{
			m_Canvas = canvas;
			m_PanelManager = panelManager;
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
				m_PanelManager.Add(this, m_Canvas);
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
				m_PanelManager.Remove(this);
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
