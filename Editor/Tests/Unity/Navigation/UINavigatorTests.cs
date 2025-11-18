using NUnit.Framework;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity.Navigation
{
	[Category(UITestsConsts.CATEGORY_ASSEMBLY)]
	public class UINavigatorTests
	{
		[Test]
		public void Navigate_Empty()
		{
			UINavigator navigator = new UINavigator();

			Assert.IsNull(navigator.Selected);
			foreach(EGridCardinal2D direction in EGridCardinal2DExt.Meta.Values)
			{
				Assert.DoesNotThrow(() => navigator.Navigate(direction));
			}
			Assert.IsNull(navigator.Selected);
		}

		[Test]
		public void Navigate_SingleEntry_NothingSelectedBefore()
		{
			UINavigator navigator = new UINavigator();
			TestNavigationReceiver receiver = new TestNavigationReceiver();
			UINavigationTarget target = navigator.Add(receiver);

			navigator.Navigate(EGridCardinal2D.Up);
			Assert.AreEqual(target, navigator.Selected);
			AssertReceiver(receiver, 1, 0, 0);
		}

		[Test]
		public void Navigate_ListOfEntries()
		{
			UINavigator navigator = new UINavigator();
			TestNavigationReceiver receiverA = new TestNavigationReceiver();
			TestNavigationReceiver receiverB = new TestNavigationReceiver();
			UINavigationTarget targetA = navigator.Add(receiverA);
			UINavigationTarget targetB = navigator.Add(receiverB);
			targetA.LinkTo(targetB, EGridCardinal2D.Right);

			navigator.Navigate(EGridCardinal2D.Right);
			Assert.AreEqual(targetA, navigator.Selected);
			AssertReceiver(receiverA, 1, 0, 0);
			AssertReceiver(receiverB, 0, 0, 0);
			navigator.Navigate(EGridCardinal2D.Right);
			Assert.AreEqual(targetB, navigator.Selected);
			AssertReceiver(receiverA, 1, 1, 0);
			AssertReceiver(receiverB, 1, 0, 0);
		}

		[Test]
		public void SetDefault()
		{
			UINavigator navigator = new UINavigator();
			TestNavigationReceiver receiverA = new TestNavigationReceiver();
			TestNavigationReceiver receiverB = new TestNavigationReceiver();
			UINavigationTarget targetA = navigator.Add(receiverA);
			UINavigationTarget targetB = navigator.Add(receiverB);

			navigator.SetDefault(targetB);
			navigator.Navigate(EGridCardinal2D.Right);
			Assert.AreEqual(targetB, navigator.Selected);
		}

		[Test]
		public void SetSelected()
		{
			UINavigator navigator = new UINavigator();
			TestNavigationReceiver receiverA = new TestNavigationReceiver();
			TestNavigationReceiver receiverB = new TestNavigationReceiver();
			UINavigationTarget targetA = navigator.Add(receiverA);
			UINavigationTarget targetB = navigator.Add(receiverB);

			navigator.SetSelected(receiverA);
			Assert.AreEqual(targetA, navigator.Selected);
			AssertReceiver(receiverA, 1, 0, 0);
			AssertReceiver(receiverB, 0, 0, 0);
			navigator.SetSelected(receiverB);
			Assert.AreEqual(targetB, navigator.Selected);
			AssertReceiver(receiverA, 1, 1, 0);
			AssertReceiver(receiverB, 1, 0, 0);
		}

		[Test]
		public void SetSelected_ToNull()
		{
			UINavigator navigator = new UINavigator();
			TestNavigationReceiver receiver = new TestNavigationReceiver();
			UINavigationTarget target = navigator.Add(receiver);

			navigator.SetSelected(receiver);
			AssertReceiver(receiver, 1, 0, 0);
			navigator.SetSelected(null);
			AssertReceiver(receiver, 1, 1, 0);
		}

		[Test]
		public void AcceptSelected_Empty()
		{
			UINavigator navigator = new UINavigator();
			Assert.DoesNotThrow(() => navigator.AcceptSelected());
		}

		[Test]
		public void AcceptSelected_NothingSelected()
		{
			TestNavigationReceiver receiver = new TestNavigationReceiver();
			UINavigator navigator = new UINavigator();
			navigator.Add(receiver);

			Assert.AreEqual(0, receiver.AcceptedCount);
			navigator.AcceptSelected();
			Assert.AreEqual(0, receiver.AcceptedCount);
		}

		[Test]
		public void AcceptSelected_AcceptedCallback()
		{
			TestNavigationReceiver receiver = new TestNavigationReceiver();
			UINavigator navigator = new UINavigator();
			navigator.Add(receiver);
			navigator.SetSelected(receiver);
			AssertReceiver(receiver, 1, 0, 0);

			navigator.AcceptSelected();
			AssertReceiver(receiver, 1, 0, 1);
		}

		private void AssertReceiver(TestNavigationReceiver receiver, int selectedCount, int deselectedCount, int acceptedCount)
		{
			Assert.AreEqual(selectedCount, receiver.SelectedCount);
			Assert.AreEqual(deselectedCount, receiver.DeselectedCount);
			Assert.AreEqual(acceptedCount, receiver.AcceptedCount);
		}
	}
}
