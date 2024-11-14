using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity
{
	public enum ELayoutAxis
	{
		Vertical = 0,
		Horizontal = 1
	}

	public static class ELayoutAxisExt
	{
		public static readonly EnumExt<ELayoutAxis> Meta = new EnumExt<ELayoutAxis>();
	}
}
