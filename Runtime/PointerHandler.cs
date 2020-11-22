using ProceduralLevel.Common.Event;

namespace ProceduralLevel.UnityPlugins.CustomUI
{
	public class PointerHandler
	{
		private readonly EPointerStatus[] m_PointerStatus = new EPointerStatus[EPointerTypeExt.MAX_VALUE];

		public readonly CustomEvent<EPointerType, bool> OnChanged = new CustomEvent<EPointerType, bool>();

		public void Use(EPointerType pointer)
		{
			int index = (int)pointer;
			EPointerStatus oldStatus = m_PointerStatus[index];
			m_PointerStatus[index] = EPointerStatus.Active;
			if(oldStatus == EPointerStatus.Idle)
			{
				OnChanged.Invoke(pointer, true);
			}
		}

		public bool UpdateStatus()
		{
			bool anyChange = false;
			int length = m_PointerStatus.Length;
			for(int x = 0; x < length; ++x)
			{
				EPointerStatus oldStatus = m_PointerStatus[x];
				if(oldStatus == EPointerStatus.Idle)
				{
					continue;
				}
				anyChange = true;
				--m_PointerStatus[x];

				if(oldStatus == EPointerStatus.Awaiting)
				{
					EPointerType pointer = (EPointerType)x;
					OnChanged.Invoke(pointer, false);
				}
			}
			return anyChange;
		}

		public bool IsActive(EPointerType pointer)
		{
			return m_PointerStatus[(int)pointer] != EPointerStatus.Idle;
		}

		public bool IsActive()
		{
			int length = m_PointerStatus.Length;
			for(int x = 0; x < length; ++x)
			{
				if(m_PointerStatus[x] != EPointerStatus.Idle)
				{
					return true;
				}
			}
			return false;
		}
	}
}
