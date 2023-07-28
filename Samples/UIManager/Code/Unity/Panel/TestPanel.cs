using ProceduralLevel.Common.Event;
using ProceduralLevel.Common.Unity;
using ProceduralLevel.UI.Unity;
using UnityEngine;

namespace ProceduralLevel.UI.Samples
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

		private void OnGUI()
		{
			Rect rect = new Rect(0, 0, 100, 100);
			GUICanvas canvas = new GUICanvas(1920, 1080);
			canvas.Use();
			GUI.Box(rect, GUIContent.none);
			GUI.Button(new Rect(canvas.Width-100f, canvas.Height-100f, 100f, 100f), $"Hello World!");
		}
	}
}
