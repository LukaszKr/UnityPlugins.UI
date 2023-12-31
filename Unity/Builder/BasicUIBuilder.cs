using ProceduralLevel.Localization.Unity;

namespace ProceduralLevel.UI.Unity
{
	public class BasicUIBuilder : UIBuilder
	{
		public readonly BasicUIBuilderConfig Config;

		public BasicUIBuilder(BasicUIBuilderConfig config)
		{
			Config = config;
		}

		public UILabel Label(string name, string text)
		{
			UILabel label = Create(name).SetStatic(50).Spawn(Config.Label);
			label.SetText(text);
			return label;
		}

		public UILabel Label(string name, LocalizationKey key)
		{
			UILabel label = Create(name).SetStatic(50).Spawn(Config.Label);
			label.SetLocalization(key);
			return label;
		}
	}
}
