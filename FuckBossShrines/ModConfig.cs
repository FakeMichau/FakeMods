using BepInEx.Configuration;

namespace FuckBossShrines;
internal class ModConfig
{
    public static ConfigEntry<bool> DisableShrines;

    public static void InitConfig(ConfigFile config)
    {
        DisableShrines = config.Bind("Settings", "Disable Shrines", defaultValue: true, "");
    }
}
