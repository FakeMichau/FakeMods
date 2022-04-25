using BepInEx.Bootstrap;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;

namespace BetterBossLootDropping
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
            ModSettingsManager.AddOption(new CheckBoxOption(ModConfig.DisableInSingle, new CheckBoxConfig { checkIfDisabled = CheckMod }));
            ModSettingsManager.AddOption(new CheckBoxOption(ModConfig.RoundDownItems, new CheckBoxConfig { checkIfDisabled = CheckMod }));

            ModSettingsManager.AddOption(new CheckBoxOption(ModConfig.LootIntoInventory, new CheckBoxConfig { checkIfDisabled = CheckMod }));
            ModSettingsManager.AddOption(new CheckBoxOption(ModConfig.LunarIntoInventory, new CheckBoxConfig { checkIfDisabled = CheckLII }));
            ModSettingsManager.AddOption(new CheckBoxOption(ModConfig.DropRedItems, new CheckBoxConfig { checkIfDisabled = CheckLII }));

            ModSettingsManager.AddOption(new StepSliderOption(ModConfig.DelayLength, new StepSliderConfig{ min = 0, max = 1, increment = (float)0.05, checkIfDisabled = CheckDrop }));

            ModSettingsManager.SetModDescription("Enhanced way of getting boss loot.");
        }

        private static bool CheckDrop()
        {
            return ModConfig.DisableMod.Value || ModConfig.LootIntoInventory.Value;
        }
        private static bool CheckMod()
        {
            return ModConfig.DisableMod.Value;
        }
        private static bool CheckLII()
        {
            return ModConfig.DisableMod.Value || !ModConfig.LootIntoInventory.Value;
        }
    }
}
