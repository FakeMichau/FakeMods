using BepInEx;
using R2API.Utils;
using RoR2;
using UnityEngine;

namespace FuckBossShrines
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    public class FuckBossShrines : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "FakeMichau";
        public const string PluginName = "FuckBossShrines";
        public const string PluginVersion = "1.0.0";

        public void Awake()
        {
            ModConfig.InitConfig(Config);
            On.RoR2.Stage.Start += Stage_Start;

            if (RiskOfOptionsCompat.Enabled)
            {
                RiskOfOptionsCompat.AddOptions();
            }
        }

        private void Stage_Start(On.RoR2.Stage.orig_Start orig, Stage self)
        {
            orig(self);
        }
    }
}
