using UnityPlugins.Common.Logic;

namespace UnityPlugins.UI.Unity
{
	public class UINavigationTarget
	{
		public readonly INavigationReceiver Receiver;
		public readonly UINavigationLink[] Links;

		public UINavigationTarget(INavigationReceiver receiver)
		{
			Receiver = receiver;
			Links = new UINavigationLink[EGridCardinal2DExt.Meta.Values.Length];
			for(int x = 0; x < Links.Length; ++x)
			{
				Links[x] = new UINavigationLink();
			}
		}

		public void LinkTo(UINavigationTarget target, EGridCardinal2D direction, bool biDirectional = true)
		{
			UINavigationLink link = Links[(int)direction];
			link.Add(target);
			if(biDirectional)
			{
				target.LinkTo(this, direction.GetOpposite(), false);
			}
		}

		public UINavigationTarget GetTargetInDirection(EGridCardinal2D direction)
		{
			UINavigationLink link = Links[(int)direction];
			UINavigationTarget target = link.GetValidTarget(true);
			if(target == null)
			{
				return this;
			}
			if(!target.Receiver.IsNavigationActive)
			{
				target = target.GetTargetInDirection(direction);
			}

			if(target != null && target.Receiver.IsNavigationActive)
			{
				return target;
			}

			return this;
		}

		public override string ToString()
		{
			return $"[{Receiver}, Links: {Links.Length}]";
		}
	}
}
