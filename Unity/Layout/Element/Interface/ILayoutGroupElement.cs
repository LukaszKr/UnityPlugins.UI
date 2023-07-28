using System.Collections.Generic;

namespace ProceduralLevel.UI.Unity
{
	public interface ILayoutGroupElement
	{
		IEnumerable<ALayoutElement> GetElements();
		void DoLayout();
	}
}
