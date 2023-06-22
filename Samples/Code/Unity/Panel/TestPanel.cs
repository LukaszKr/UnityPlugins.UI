using ProceduralLevel.Common.Event;
using ProceduralLevel.UnityPlugins.UI.Unity;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Example
{
	public class TestPanel : APanel
	{
		protected override void OnInitialize(EventBinder binder)
		{

		}

		public new void Show()
		{
			base.Show();
		}
	}
}
