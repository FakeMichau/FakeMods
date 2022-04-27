using BepInEx;
using HG;
using R2API.Utils;
using RoR2;

namespace CheatersGoBrr
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    public class CheatersGoBrr : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "FakeMichau";
        public const string PluginName = "CheatersGoBrr";
        public const string PluginVersion = "1.0.0";

        public void Awake()
        {
            ModConfig.InitConfig(Config);
            On.RoR2.NetworkUser.DeductLunarCoins += NetworkUser_DeductLunarCoins;
            On.RoR2.LunarCoinDef.GrantPickup += LunarCoinDef_GrantPickup;
            On.EntityStates.NewtMonster.SpawnState.OnEnter += SpawnState_OnEnter;

            if (RiskOfOptionsCompat.Enabled)
            {
                RiskOfOptionsCompat.AddOptions();
            }
        }

        private void SpawnState_OnEnter(On.EntityStates.NewtMonster.SpawnState.orig_OnEnter orig, EntityStates.NewtMonster.SpawnState self)
        {
            orig(self);
            Utils.SendMessage(ModConfig.OnEntryMessage.Value, "#4f8aff");
        }
        private void LunarCoinDef_GrantPickup(On.RoR2.LunarCoinDef.orig_GrantPickup orig, LunarCoinDef self, ref PickupDef.GrantContext context)
        {
            NetworkUser networkUser = Util.LookUpBodyNetworkUser(context.body);
            if (networkUser)
            {
                if (networkUser.lunarCoins >= ModConfig.Threshold.Value && ModConfig.PreventStealingLunars.Value)
                {
                    Utils.SendMessage(ModConfig.PSLMessage.Value, "#ffc0cb");
                    context.shouldNotify = false;
                    context.shouldDestroy = false;
                    return;
                }
            }
            orig(self, ref context);
        }

        private void NetworkUser_DeductLunarCoins(On.RoR2.NetworkUser.orig_DeductLunarCoins orig, NetworkUser self, uint count)
        {
            if (self.netLunarCoins >= ModConfig.Threshold.Value) 
            {
                if (count == 1 << ModConfig.WarningPricePower.Value)
                {
                    Utils.SendMessage(ModConfig.WarningMessage.Value, "#ff7f7f");
                    self.master.inventory.GiveItemString("TonicAffliction");
                }
                else if (ModConfig.GiveTonicAffliction.Value) 
                {
                    self.master.inventory.GiveItemString("TonicAffliction");
                }
                if (count >= 1 << ModConfig.RemoveAllPricePower.Value) 
                {
                    Utils.SendMessage(ModConfig.RALMessage.Value, "#ffc0cb");
                    count = self.netLunarCoins;
                }
                else 
                {
                    var multiplier = Convert.ToUIntClamped(ModConfig.DeductMultiplier.Value < 1 ? 1 : ModConfig.DeductMultiplier.Value);
                    count *= multiplier;
                }
                self.NetworknetLunarCoins = HGMath.UintSafeSubtract(self.netLunarCoins, count);
                self.CallRpcDeductLunarCoins(count);
            }
            else 
            {
                orig(self, count);
            }
        }
    }
}
