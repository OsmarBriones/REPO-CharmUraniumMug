using CharmCore.Api;

namespace CharmUraniumMug
{
	/// <summary>Warm Glow: you slowly regenerate health while crouching.</summary>
	internal sealed class UraniumMugCharm : Charm
	{
		internal const string PrefabName = "Valuable Manor Uranium Mug";

		internal UraniumMugCharm()
			: base("UraniumMug", "Uranium Mug - Warm Glow", defaultWearPercent: 1f, defaultCooldownSeconds: null, PrefabName)
		{
			// Extra host-synced numbers for this power go here, for example:
			// Amount = AddSetting("Amount", 30f, 1f, 100f, "Description for the config file.");
		}
	}
}
