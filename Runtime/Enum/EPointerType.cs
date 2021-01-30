using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public enum EPointerType
	{
		Primary = 0,
		Secondary = 1,
		Tertiary = 2,
		Quaternary = 3,
	}

	public static class EPointerTypeExt
	{
		public static readonly EnumExt<EPointerType> Meta = new EnumExt<EPointerType>();
	}
}
