using BepInEx.Configuration;

namespace FuckBossShrines;
internal class ModConfig
{
    public static ConfigEntry<bool> DisableMod;
    public static ConfigEntry<string> Message;

    public static void InitConfig(ConfigFile config)
    {
        DisableMod = config.Bind("Settings", "Disable Mod", defaultValue: false, "");
        Message = config.Bind("Settings", "Message", defaultValue: "FUCK YOU!", "");
    }
}
