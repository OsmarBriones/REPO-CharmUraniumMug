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
			// WearPercent means percent per SECOND of actual healing for this charm.
			WearsPerSecond = true;
			HealIntervalSeconds = AddSetting("HealIntervalSeconds", 3f, 0.5f, 30f, "Seconds of crouching between two heals.");
			HealAmount = AddSetting("HealAmount", 1f, 1f, 20f, "Health points restored on each heal (whole points).");
		}

		/// <summary>Seconds of crouching between two heals.</summary>
		internal CharmSetting HealIntervalSeconds { get; }

		/// <summary>Health points restored on each heal (rounded to a whole number when used).</summary>
		internal CharmSetting HealAmount { get; }
	}
}
