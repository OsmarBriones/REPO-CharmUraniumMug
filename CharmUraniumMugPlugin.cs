using BepInEx;
using CharmCore.Api;
using UnityEngine;

namespace CharmUraniumMug
{
	[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
	[BepInDependency("com.osmar.Pockets", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("com.osmar.CharmCore", BepInDependency.DependencyFlags.HardDependency)]
	public class CharmUraniumMugPlugin : BaseUnityPlugin
	{
		public const string PluginGuid = "com.osmar.CharmUraniumMug";
		public const string PluginName = "CharmUraniumMug";
		public const string PluginVersion = "1.0.0";

		internal static new BepInEx.Logging.ManualLogSource Logger { get; private set; } = null!;

		private void Awake()
		{
			Logger = base.Logger;

			// Prevent the plugin from being deleted
			this.gameObject.transform.parent = null;
			this.gameObject.hideFlags = HideFlags.HideAndDontSave;

			// Registering binds Enabled, WearPercent, CooldownSeconds and the Debug section (DebugMode) in this mod's config file.
			UraniumMugCharm charm = new UraniumMugCharm();
			CharmsApi.Register(charm, Config);
			WarmGlow glow = this.gameObject.AddComponent<WarmGlow>();
			glow.Charm = charm;
			charm.Glow = glow;

			Logger.LogInfo($"{PluginName} {PluginVersion} loaded!");
		}
	}
}
