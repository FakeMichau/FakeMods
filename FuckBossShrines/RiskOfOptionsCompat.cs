﻿using BepInEx.Bootstrap;
using RiskOfOptions;
using RiskOfOptions.Options;

namespace FuckBossShrines
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
            ModSettingsManager.AddOption(new StringInputFieldOption(ModConfig.Message));

            //ModSettingsManager.SetModDescription("");
        }
    }
}