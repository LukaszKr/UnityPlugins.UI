using ProceduralLevel.UnityPlugins.Common.Unity.Extended;
using ProceduralLevel.UnityPlugins.UI.Unity;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.UI.Example
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
