using BepInEx;
using R2API.Utils;
using RoR2;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BetterBossLootDropping
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    public class BetterBossLootDropping : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "FakeMichau";
        public const string PluginName = "BetterBossLootDropping";
        public const string PluginVersion = "1.0.0";

        public void Awake()
        {
            ModConfig.InitConfig(Config);
            On.RoR2.BossGroup.DropRewards += BossGroup_DropRewards;

            if (RiskOfOptionsCompat.Enabled)
            {
                RiskOfOptionsCompat.AddOptions();
            }
        }

        private void BossGroup_DropRewards(On.RoR2.BossGroup.orig_DropRewards orig, BossGroup self) {
            ArtifactDef artifactDef = ArtifactCatalog.FindArtifactDef("Command");
            if (RunArtifactManager.instance.IsArtifactEnabled(artifactDef) || (RoR2Application.isInSinglePlayer && ModConfig.DisableInSingle.Value) || ModConfig.DisableMod.Value)
            {
                orig(self);
                return;
            }

            int livingPlayerCount = Run.instance.livingPlayerCount;
            int participatingPlayerCount = Run.instance.participatingPlayerCount;
            if (participatingPlayerCount == 0 || !self.dropPosition) return;
            PickupIndex pickupIndex;
            if (self.dropTable)
            {
                pickupIndex = self.dropTable.GenerateDrop(self.rng);
            }
            else
            {
                List<PickupIndex> list = Run.instance.availableTier2DropList;
                if (self.forceTier3Reward)
                {
                    list = Run.instance.availableTier3DropList;
                }
                pickupIndex = self.rng.NextElementUniform<PickupIndex>(list);
            }

            int num = 1 + self.bonusRewardCount;
            if (self.scaleRewardsByPlayerCount)
            {
                num *= participatingPlayerCount;
            }

            // round down number of items per living player
            if (ModConfig.RoundDownItems.Value) {
                num /= livingPlayerCount;
                num *= livingPlayerCount;
            }

            float angle = 360f / num;
            Vector3 vector = Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), Vector3.up) * (Vector3.up * 40f + Vector3.forward * 5f);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);

            // get list of living players
            var livingPlayersList = new List<PlayerCharacterMasterController>();
            using (var enumerator = PlayerCharacterMasterController._instances.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current != null && enumerator.Current.master.hasBody)
                    {
                        livingPlayersList.Add(enumerator.Current);
                    }
                }
            }

            var shuffledLivingPlayersList = livingPlayersList.OrderBy(item => self.rng.Next()).ToList();

            // give every living player an item
            int counter = 0;
            for (int i = 0; i < num; i++)
            {
                PickupIndex pickupIndex2 = pickupIndex;
                if ((self.bossDrops.Count > 0 || self.bossDropTables.Count > 0) && self.rng.nextNormalizedFloat <= self.bossDropChance)
                {
                    pickupIndex2 = self.bossDropTables.Count > 0 ? self.rng.NextElementUniform<PickupDropTable>(self.bossDropTables).GenerateDrop(self.rng) : self.rng.NextElementUniform<PickupIndex>(self.bossDrops);
                }

                if (ModConfig.LootIntoInventory.Value)
                {
                    if (ModConfig.DropRedItems.Value &&
                        SceneManager.GetActiveScene().name.ToLower() == "shipgraveyard" &&
                        self.name.StartsWith("SuperRoboBallEncounter"))
                    {
                        PickupDropletController.CreatePickupDroplet(pickupIndex2, self.dropPosition.position, vector);
                        vector = rotation * vector;
                    }
                    else
                    {
                        if (pickupIndex2.pickupDef.isLunar && !ModConfig.LunarIntoInventory.Value)
                        {
                            PickupDropletController.CreatePickupDroplet(
                                pickupIndex2,
                                shuffledLivingPlayersList[counter].master.GetBodyObject().transform.position,
                                new Vector3(self.rng.nextNormalizedFloat, self.rng.nextNormalizedFloat, self.rng.nextNormalizedFloat
                                ));
                        }
                        else
                        {
                            shuffledLivingPlayersList[counter].master.inventory.GiveItem(pickupIndex2.pickupDef.itemIndex);
                            GenericPickupController.SendPickupMessage(shuffledLivingPlayersList[counter].master, pickupIndex2);
                        }
                        counter++;
                        if (counter >= shuffledLivingPlayersList.Count) counter = 0;
                    }
                }
                else
                {
                    StartCoroutine(Utils.DelayedDrop(pickupIndex2, self.dropPosition.position, vector, ModConfig.DelayLength.Value * i));
                    vector = rotation * vector;
                }
            }
        }
    }
}
