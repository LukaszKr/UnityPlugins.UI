using System.Collections.Generic;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity
{
	public class UINavigator
	{
		private readonly Dictionary<INavigationReceiver, UINavigationTarget> m_ElementLookup = new Dictionary<INavigationReceiver, UINavigationTarget>();
		private readonly List<UINavigationTarget> m_Targets = new List<UINavigationTarget>();
		private UINavigationTarget m_DefaultTarget;
		private UINavigationTarget m_Selected;

		private readonly EventBinder m_Binder = new EventBinder();

		public UINavigationTarget Selected => m_Selected;
		public IReadOnlyList<UINavigationTarget> Targets => m_Targets;

		public readonly CustomEvent<UINavigationTarget> OnCurrentChanged = new CustomEvent<UINavigationTarget>();

		public void Clear()
		{
			m_Targets.Clear();
			m_ElementLookup.Clear();
			m_DefaultTarget = null;
			m_Selected = null;

			m_Binder.UnbindAll();
		}

		#region Build navigation
		public UINavigationTarget Add(INavigationReceiver receiver)
		{
			UINavigationTarget target = new UINavigationTarget(receiver);
			m_ElementLookup.Add(receiver, target);
			m_Binder.Bind(receiver.OnNavigationSelected, OnReceiverSelectedHandler);
			m_Targets.Add(target);
			return target;
		}

		public UINavigationTarget[] Add(params INavigationReceiver[] targets)
		{
			UINavigationTarget[] navTargets = new UINavigationTarget[targets.Length];
			for(int x = 0; x <  navTargets.Length; ++x)
			{
				navTargets[x] = Add(targets[x]);
			}
			return navTargets;
		}

		public UINavigationTarget[] Add(INavigationReceiver[] targets, EGridCardinal2D direction, bool wrap = true)
		{
			UINavigationTarget[] navTargets = Add(targets);
			Link(navTargets, direction, wrap);
			return navTargets;
		}

		public void Link(IReadOnlyList<UINavigationTarget> navTargets, EGridCardinal2D direction, bool wrap = true)
		{
			int count = navTargets.Count;
			int lastIndex = (wrap? count: count-1);
			for(int x = 0; x < lastIndex; ++x)
			{
				int next = (x+1) % count;
				navTargets[x].LinkTo(navTargets[next], direction);
			}
		}

		public void SetDefault(UINavigationTarget defaultTarget)
		{
			m_DefaultTarget = defaultTarget;
		}
		#endregion

		#region Navigation
		public bool Navigate(EGridCardinal2D direction)
		{
			UINavigationTarget target = null;
			if(m_Selected == null)
			{
				if(m_Targets.Count == 0)
				{
					return false;
				}

				if(m_DefaultTarget != null)
				{
					return SetSelected(m_DefaultTarget);
				}

				return SetSelected(m_Targets[0]);
			}

			target = m_Selected.GetTargetInDirection(direction);
			return SetSelected(target);
		}

		public bool SetSelected(INavigationReceiver receiver)
		{
			if(receiver == null)
			{
				return SetSelected((UINavigationTarget)null);
			}
			else
			{
				UINavigationTarget target = m_ElementLookup[receiver];
				return SetSelected(target);
			}
		}

		private bool SetSelected(UINavigationTarget target)
		{
			if(target == m_Selected)
			{
				return false;
			}

			if(m_Selected != null)
			{
				m_Selected.Receiver.NavigationDeselected();
			}
			UINavigationTarget prev = m_Selected;
			m_Selected = target;
			if(m_Selected != null)
			{
				m_Selected.Receiver.NavigationSelected();
			}
			OnCurrentChanged.Invoke(target);
			return prev != m_Selected;
		}

		public void AcceptSelected()
		{
			if(m_Selected == null)
			{
				return;
			}
			m_Selected.Receiver.NavigationAccepted();
		}

		public void Select()
		{
			if(m_Selected == null)
			{
				if(m_DefaultTarget != null)
				{
					SetSelected(m_DefaultTarget);
				}
				else if(m_Targets.Count > 0)
				{
					SetSelected(m_Targets[0]);
				}
				return;
			}

			m_Selected.Receiver.NavigationSelected();
		}

		public void Deselect()
		{
			if(m_Selected == null)
			{
				return;
			}
			m_Selected.Receiver.NavigationDeselected();
		}
		#endregion

		#region Callbacks
		private void OnReceiverSelectedHandler(INavigationReceiver receiver)
		{
			SetSelected(receiver);
		}
		#endregion
	}
}
