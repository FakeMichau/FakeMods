using RoR2;

namespace BossLootIntoInventory
{
    public static class Commands
    {
        // to be removed
        public static bool DisableMod = false;

        [ConCommand(commandName = "bl_disable", flags = ConVarFlags.ExecuteOnServer,
            helpText = "Disables 'Boss Loot Into Inventory' mod.")]
        private static void CCDisableMod(ConCommandArgs args)
        {
            DisableMod = true;
        }
        [ConCommand(commandName = "bl_enable", flags = ConVarFlags.ExecuteOnServer,
            helpText = "Enables 'Boss Loot Into Inventory' mod.")]
        private static void CCEnableMod(ConCommandArgs args)
        {
            DisableMod = false;
        }
    }
}
