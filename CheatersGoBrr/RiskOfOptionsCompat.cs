using BepInEx.Bootstrap;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;

namespace CheatersGoBrr
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
            ModSettingsManager.AddOption(new IntSliderOption(ModConfig.Threshold, new IntSliderConfig { min = 0, max = 100000 }));
            ModSettingsManager.AddOption(new IntSliderOption(ModConfig.DeductMultiplier, new IntSliderConfig { min = 1, max = 10000 }));
            ModSettingsManager.AddOption(new IntSliderOption(ModConfig.WarningPricePower, new IntSliderConfig { min = 0, max = 25 }));
            ModSettingsManager.AddOption(new IntSliderOption(ModConfig.RemoveAllPricePower, new IntSliderConfig { min = 0, max = 25 }));
            ModSettingsManager.AddOption(new CheckBoxOption(ModConfig.GiveTonicAffliction));
            ModSettingsManager.AddOption(new CheckBoxOption(ModConfig.PreventStealingLunars));
            ModSettingsManager.AddOption(new StringInputFieldOption(ModConfig.PSLMessage));
            ModSettingsManager.AddOption(new StringInputFieldOption(ModConfig.RALMessage));

            ModSettingsManager.SetModDescription("We hate cheaters.");
        }
    }
}
