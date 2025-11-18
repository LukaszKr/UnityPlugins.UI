using NUnit.Framework;

namespace UnityPlugins.UI.Unity.Navigation
{
	[Category(UITestsConsts.CATEGORY_ASSEMBLY)]
	public class UINavigationLinkTests
	{
		[Test]
		public void GetValidTarget_AddThenRemove()
		{
			UINavigationLink link = new UINavigationLink();
			UINavigationTarget expected = new UINavigationTarget(new TestNavigationReceiver(true));
			link.Add(expected);
			Assert.AreEqual(expected, link.GetValidTarget());
			link.Remove(expected);
			Assert.IsNull(link.GetValidTarget());
		}

		[Test]
		public void GetValidTarget_EmptyList()
		{
			UINavigationLink link = new UINavigationLink();
			Assert.IsNull(link.GetValidTarget());
		}

		[Test]
		public void GetValidTarget_OnlyDisabledTargets()
		{
			UINavigationLink link = new UINavigationLink();
			link.Add(new UINavigationTarget(new TestNavigationReceiver(false)));
			link.Add(new UINavigationTarget(new TestNavigationReceiver(false)));
			Assert.IsNull(link.GetValidTarget());
		}

		[Test]
		public void GetValidTarget_DisabledAndActiveTargets()
		{
			UINavigationLink link = new UINavigationLink();
			link.Add(new UINavigationTarget(new TestNavigationReceiver(false)));
			UINavigationTarget expected = new UINavigationTarget(new TestNavigationReceiver(true));
			link.Add(expected);
			Assert.AreEqual(expected, link.GetValidTarget());
		}

		[Test]
		public void GetValidTarget_TargetBecomesActive()
		{
			UINavigationLink link = new UINavigationLink();
			TestNavigationReceiver receiver = new TestNavigationReceiver(false);
			UINavigationTarget target = new UINavigationTarget(receiver);
			link.Add(target);
			Assert.IsNull(link.GetValidTarget());
			receiver.IsNavigationActive = true;
			Assert.AreEqual(target, link.GetValidTarget());
		}
	}
}
