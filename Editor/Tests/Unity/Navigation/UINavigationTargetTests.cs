using NUnit.Framework;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity.Navigation
{
	[Category(UITestsConsts.CATEGORY_ASSEMBLY)]
	public class UINavigationTargetTests
	{
		[Test]
		public void GetTargetInDirection_NoLinksCreated_AlwaysReturnSelf()
		{
			UINavigationTarget target = new UINavigationTarget(new TestNavigationReceiver());
			EGridCardinal2D[] directions = EGridCardinal2DExt.Meta.Values;
			foreach(EGridCardinal2D direction in directions)
			{
				Assert.AreEqual(target, target.GetTargetInDirection(direction));
			}
		}

		[Test]
		public void GetTargetInDirection_SkipDisabledTargets()
		{
			UINavigationTarget target = new UINavigationTarget(new TestNavigationReceiver(true, "Target"));
			TestNavigationReceiver inactiveReceiver = new TestNavigationReceiver(false, "Inactive");
			UINavigationTarget disabledTarget = new UINavigationTarget(inactiveReceiver);
			UINavigationTarget activeTarget = new UINavigationTarget(new TestNavigationReceiver(true, "Active"));

			target.LinkTo(disabledTarget, EGridCardinal2D.Right);
			Assert.AreEqual(target, target.GetTargetInDirection(EGridCardinal2D.Right));
			target.LinkTo(activeTarget, EGridCardinal2D.Right);
			Assert.AreEqual(activeTarget, target.GetTargetInDirection(EGridCardinal2D.Right));
		}

		[Test]
		public void GetTargetInDirection_JumpOverDisabledTargets()
		{
			TestNavigationReceiver inactiveReceiver = new TestNavigationReceiver(false);

			UINavigationTarget target = new UINavigationTarget(new TestNavigationReceiver());
			UINavigationTarget disabledTarget = new UINavigationTarget(inactiveReceiver);
			UINavigationTarget activeTarget = new UINavigationTarget(new TestNavigationReceiver());

			target.LinkTo(disabledTarget, EGridCardinal2D.Right);
			disabledTarget.LinkTo(activeTarget, EGridCardinal2D.Right);

			Assert.AreEqual(activeTarget, target.GetTargetInDirection(EGridCardinal2D.Right));
		}

		[Test]
		public void LinkTo_Bidirectional()
		{
			UINavigationTarget targetA = new UINavigationTarget(new TestNavigationReceiver());
			UINavigationTarget targetB = new UINavigationTarget(new TestNavigationReceiver());
			targetA.LinkTo(targetB, EGridCardinal2D.Right);
			Assert.AreEqual(targetB, targetA.GetTargetInDirection(EGridCardinal2D.Right));
			Assert.AreEqual(targetA, targetB.GetTargetInDirection(EGridCardinal2D.Left));
		}

		[Test]
		public void LinkTo_NotBidirectional()
		{
			UINavigationTarget targetA = new UINavigationTarget(new TestNavigationReceiver());
			UINavigationTarget targetB = new UINavigationTarget(new TestNavigationReceiver());
			targetA.LinkTo(targetB, EGridCardinal2D.Right, false);
			Assert.AreEqual(targetB, targetA.GetTargetInDirection(EGridCardinal2D.Right));
			Assert.AreEqual(targetB, targetB.GetTargetInDirection(EGridCardinal2D.Left));
		}
	}
}
