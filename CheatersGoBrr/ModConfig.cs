using BepInEx.Configuration;

namespace CheatersGoBrr;
internal class ModConfig
{
    public static ConfigEntry<int> Threshold;
    public static ConfigEntry<int> DeductMultiplier;
    public static ConfigEntry<int> WarningPricePower;
    public static ConfigEntry<int> RemoveAllPricePower;
    public static ConfigEntry<bool> GiveTonicAffliction;
    public static ConfigEntry<bool> PreventStealingLunars;
    public static ConfigEntry<string> PSLMessage;
    public static ConfigEntry<string> RALMessage;
    public static ConfigEntry<string> WarningMessage;

    public static void InitConfig(ConfigFile config)
    {
        Threshold = config.Bind("Settings", "Threshold", defaultValue: 5000, "Having how many lunars will activate the effects.");
        DeductMultiplier = config.Bind("Effects", "Deduct Multiplier", defaultValue: 1, "Multiplies the cost of lunar items.\n1 effectively disables the effect.");
        WarningPricePower = config.Bind("Effects", "Warning Price", defaultValue: 9, "Price at which there will be a warning.\nThis setting works in powers of 2. Value 9 means the effective value will be 2^9 meaning 512.");
        WarningMessage = config.Bind("Effects", "Warning Message", defaultValue: "It's dangerous to feed the Lunar Stone so much!\nHere's a tonic for you to come down.", "Message that will appear in chat as a warning");
        RemoveAllPricePower = config.Bind("Effects", "Remove All Lunar Price", defaultValue: 11, "At or above that price, while trying to spend lunars, all lunars will be removed from the player.\nThis setting works in powers of 2. Value 11 means the effective value will be 2^11 meaning 2048");
        RALMessage = config.Bind("Effects", "RAL Message", defaultValue: "YOINK!", "Optional message when taking player's all lunars.\nEmpty to disable the message.");
        GiveTonicAffliction = config.Bind("Effects", "Give Tonic Affliction", defaultValue: false, "Activates whenever a player tries to spend their lunars.");
        PreventStealingLunars = config.Bind("Effects", "Prevent Stealing Lunars", defaultValue: true, "Prevents a player from picking up a lunar coin.");
        PSLMessage = config.Bind("Effects", "PSL Message", defaultValue: "You greedy FUCK!", "Optional message when trying to pick up a lunar.\n\"Prevent Stealing Lunars\" has to be enabled. Empty to disable the message.");
    }
}
