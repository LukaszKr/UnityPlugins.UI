using ProceduralLevel.Common.Unity.Extended;
using ProceduralLevel.UI.Unity;
using UnityEngine;

namespace ProceduralLevel.UI.Samples
{
	public class UIExample : ExtendedMonoBehaviour
	{
		[SerializeField]
		private UIManager m_Manager = null;

		private void Awake()
		{
			m_Manager.Initialize();
			m_Manager.GetPanel<TestPanel>().Show();
		}
	}
}
