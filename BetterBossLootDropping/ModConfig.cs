using BepInEx.Configuration;

namespace BetterBossLootDropping;
internal class ModConfig
{
    public static ConfigEntry<bool> DisableMod;
    public static ConfigEntry<bool> DisableInSingle;
    public static ConfigEntry<bool> RoundDownItems;

    public static ConfigEntry<bool> LootIntoInventory;
    public static ConfigEntry<bool> LunarIntoInventory;
    public static ConfigEntry<bool> DropRedItems;
    
    public static ConfigEntry<float> DelayLength;

    public static void InitConfig(ConfigFile config)
    {
        DisableMod = config.Bind("Settings", "Disable the mod", defaultValue: false, "Disables the mod completely.");
        DisableInSingle = config.Bind("Settings", "Disable the mod in singleplayer", defaultValue: false, "Should the mod be disabled only while playing singleplayer.");
        RoundDownItems = config.Bind("Settings", "Round down items", defaultValue: false, "With uneven amount of players still living, should the amount of items per player be rounded down.\nThat way each player gets the same number of items every time.");

        LootIntoInventory = config.Bind("Loot into inventory", "Loot into inventory", defaultValue: true, "Should boss items go straight into inventory\nDisabling this option will enable delayed dropping of loot.");
        LunarIntoInventory = config.Bind("Loot into inventory", "Lunar items into inventory", defaultValue: false, "Should boss loot changed into lunar items via Eulogy Zero go into inventory. \n(otherwise they will drop on the ground in front of a player that rolled it)");
        DropRedItems = config.Bind("Loot into inventory", "Drop red boss items on the ground", defaultValue: false, "Applies to Siren's Call where you destroy eggs to spawn a boss that drops red items.");
        
        DelayLength = config.Bind("Delayed drop", "Delay between items dropping", defaultValue: (float)0.2, "How much time passes between each item starting to drop.\n\"Delay dropping items on the ground\" has to be enabled");
    }
}
