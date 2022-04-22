using BepInEx.Configuration;

namespace BossLootIntoInventory;
internal class ModConfig
{
    public static ConfigEntry<bool> DisableMod;
    public static ConfigEntry<bool> RoundDownItems;
    public static ConfigEntry<bool> LunarIntoInventory;
    public static ConfigEntry<bool> DisableInSingle;
    public static ConfigEntry<bool> DropRedItems;

    public static void InitConfig(ConfigFile config)
    {
        DisableMod = config.Bind("Settings", "Disable the mod", defaultValue: false, "Disables the mod completely");
        DisableInSingle = config.Bind("Settings", "Disable the mod in singleplayer", defaultValue: false, "Should the mod be disabled while playing singleplayer.");
        RoundDownItems = config.Bind("Settings", "Round down items", defaultValue: false, "With uneven amount of players still living, should the amount of items per player be rounded down.\nIn opposite to giving the rest of the items to some randomly selected players.");
        LunarIntoInventory = config.Bind("Settings", "Lunar items into inventory", defaultValue: false, "Should boss loot changed into lunar items via Eulogy Zero go into inventory. \n(otherwise they will drop on the ground in front of a player that rolled it)");
        DropRedItems = config.Bind("Settings", "Drop red boss items on the ground", defaultValue: false, "Applies to Siren's Call where you destroy eggs to spawn a boss that drops red items.");
    }
}
