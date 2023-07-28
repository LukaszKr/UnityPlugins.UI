using System.Collections.Generic;

namespace ProceduralLevel.UI.Unity
{
	public interface ILayoutGroupElement
	{
		IEnumerable<LayoutElement> GetElements();
		void DoLayout();
	}
}
