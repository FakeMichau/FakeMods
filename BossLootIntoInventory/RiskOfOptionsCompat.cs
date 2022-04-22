using BepInEx.Bootstrap;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;

namespace BossLootIntoInventory
{
    internal class RiskOfOptionsCompat
    {
        private static bool? _enabled;

        public static bool Enabled
        {
            get
            {
                _enabled ??= Chainloader.PluginInfos.ContainsKey("com.rune580.riskofoptions");
                return (bool)_enabled;
            }
        }

        public static void AddOptions()
        {
            ModSettingsManager.AddOption(new CheckBoxOption(ModConfig.DisableMod));
            ModSettingsManager.AddOption(new CheckBoxOption(ModConfig.RoundDownItems, new CheckBoxConfig { checkIfDisabled = Check }));
            ModSettingsManager.AddOption(new CheckBoxOption(ModConfig.LunarIntoInventory, new CheckBoxConfig { checkIfDisabled = Check }));
            ModSettingsManager.AddOption(new CheckBoxOption(ModConfig.DisableInSingle, new CheckBoxConfig { checkIfDisabled = Check }));
            ModSettingsManager.AddOption(new CheckBoxOption(ModConfig.DropRedItems, new CheckBoxConfig { checkIfDisabled = Check }));

            ModSettingsManager.SetModDescription("No more stealing boss loot.");
        }

        private static bool Check()
        {
            return ModConfig.DisableMod.Value;
        }
    }
}
